namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.ChangeFeed
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using GrAr.ChangeFeed;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using ProjectionHandling.LastChangedList;
    using ProjectionHandling.LastChangedList.Model;
    using Xunit;

    public class ChangeFeedServiceTests
    {
        private const int MaxPageSize = 3;
        private const string CacheKeyPrefix = "feed/building";
        private const string CacheIdSuffix = "v1.feed";
        private const string CacheLookUpUrl = "/v2/gebouwen/wijzigingen";

        [Fact]
        public async Task WhenOnTheFirstPage_ThenNothingIsMarked()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context);
            var countedPages = 0;

            await sut.MarkCompletedPageAsync(1, _ =>
            {
                countedPages++;
                return Task.FromResult(MaxPageSize);
            });

            context.LastChangedList.Should().BeEmpty();

            // There is no page before the first one, so the count is not even asked for.
            countedPages.Should().Be(0);
        }

        [Fact]
        public async Task WhenThePreviousPageIsIncomplete_ThenNothingIsMarked()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context);

            await sut.MarkCompletedPageAsync(2, _ => Task.FromResult(MaxPageSize - 1));

            context.LastChangedList.Should().BeEmpty();
        }

        [Fact]
        public async Task WhenThePreviousPageIsComplete_ThenItIsMarked()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context);

            await sut.MarkCompletedPageAsync(2, _ => Task.FromResult(MaxPageSize));

            var record = context.LastChangedList.Single();
            record.Id.Should().Be($"1.{CacheIdSuffix}");
            record.CacheKey.Should().Be($"{CacheKeyPrefix}:1");
            record.Uri.Should().Be($"{CacheLookUpUrl}?page=1");
            record.AcceptType.Should().Be("application/cloudevents-batch+json");
            record.Position.Should().Be(1);
            record.LastPopulatedPosition.Should().Be(0);
        }

        [Fact]
        public async Task WhenMarkingTheCurrentPage_ThenTheCountIsAskedForThePreviousPage()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context);
            int? countedPage = null;

            await sut.MarkCompletedPageAsync(5, page =>
            {
                countedPage = page;
                return Task.FromResult(MaxPageSize);
            });

            // The current page is still being written to and its rows are not committed yet, so the page
            // before it is the one that may be published.
            countedPage.Should().Be(4);
        }

        [Fact]
        public async Task WhenCachingIsDisabled_ThenThePageIsMarkedAsAlreadyPopulated()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context, isCacheEnabled: false);

            await sut.MarkCompletedPageAsync(2, _ => Task.FromResult(MaxPageSize));

            context.LastChangedList.Single().LastPopulatedPosition.Should().Be(1);
        }

        [Fact]
        public async Task WhenCalledAgainForTheSamePage_ThenItIsOnlyMarkedOnce()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context);
            var countedPages = 0;

            for (var i = 0; i < 3; i++)
            {
                await sut.MarkCompletedPageAsync(2, _ =>
                {
                    countedPages++;
                    return Task.FromResult(MaxPageSize);
                });
            }

            context.LastChangedList.Should().ContainSingle();

            // Every item of a page calls this, so a page that is known to be marked must not be re-queried.
            countedPages.Should().Be(1);
        }

        [Fact]
        public async Task WhenMovingOnToTheNextPage_ThenEachCompletedPageIsMarked()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context);

            await sut.MarkCompletedPageAsync(2, _ => Task.FromResult(MaxPageSize));
            await sut.MarkCompletedPageAsync(3, _ => Task.FromResult(MaxPageSize));

            context.LastChangedList
                .Select(x => x.Position)
                .Should()
                .BeEquivalentTo(new[] { 1, 2 });
        }

        [Fact]
        public async Task WhenTheRecordAlreadyExists_ThenItIsLeftUntouched()
        {
            await using var context = CreateLastChangedListContext();

            // A page can already be marked when a previous run wrote the record but crashed before the
            // projection position was committed, so the same items are projected again after a restart.
            context.LastChangedList.Add(new LastChangedRecord
            {
                Id = $"1.{CacheIdSuffix}",
                CacheKey = $"{CacheKeyPrefix}:1",
                Uri = $"{CacheLookUpUrl}?page=1",
                AcceptType = "application/cloudevents-batch+json",
                Position = 1,
                LastPopulatedPosition = 1
            });
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var sut = CreateSut(context);

            await sut.MarkCompletedPageAsync(2, _ => Task.FromResult(MaxPageSize));

            var record = context.LastChangedList.Single();
            record.LastPopulatedPosition.Should().Be(1, "the existing record must not be overwritten");
        }

        [Fact]
        public async Task WhenThePageIsMarked_ThenTheChangeTrackerIsLeftEmpty()
        {
            await using var context = CreateLastChangedListContext();
            var sut = CreateSut(context);

            await sut.MarkCompletedPageAsync(2, _ => Task.FromResult(MaxPageSize));

            // The service is typically a singleton holding this context for the life of the process. A
            // record left behind as Added would be retried by every later save on the same context.
            context.ChangeTracker.Entries().Should().BeEmpty();
        }

        private static ChangeFeedService CreateSut(LastChangedListContext context, bool isCacheEnabled = true)
            => new ChangeFeedService(
                new ChangeFeedConfig
                {
                    Namespace = "https://data.vlaanderen.be/id/gebouw",
                    FeedUrl = "https://api.basisregisters.vlaanderen.be/v2/gebouwen/wijzigingen",
                    DataSchemaUrl = "https://data.vlaanderen.be/schema/gebouw",
                    DataSchemaUrlTransform = "https://data.vlaanderen.be/schema/gebouw-transform",
                    CacheKeyPrefix = CacheKeyPrefix,
                    CacheLookUpUrl = CacheLookUpUrl,
                    CacheIdSuffix = CacheIdSuffix,
                    IsCacheEnabled = isCacheEnabled
                },
                context,
                new JsonSerializerSettings(),
                MaxPageSize);

        private static LastChangedListContext CreateLastChangedListContext()
            => new LastChangedListContext(
                new DbContextOptionsBuilder<LastChangedListContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options);
    }
}
