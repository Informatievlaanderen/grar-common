namespace Be.Vlaanderen.Basisregisters.GrAr.ChangeFeed;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudNative.CloudEvents;

public interface IChangeFeedService
{
    int MaxPageSize { get; }
    Uri FeedSourceUri { get; }
    Uri DataSchemaUri { get; }
    Uri DataSchemaUriTransform { get; }
    ChangeFeedConfig Config { get; }

    CloudEvent CreateCloudEvent(
        long feedItemId,
        DateTimeOffset timestamp,
        string eventType,
        string? objectId,
        object data,
        Uri? dataSchema,
        string eventName,
        string causationId);

    CloudEvent CreateCloudEventWithData(
        long feedItemId,
        DateTimeOffset timestamp,
        string eventType,
        string objectId,
        DateTimeOffset versionId,
        List<string> nisCodes,
        List<BaseRegistriesCloudEventAttribute> attributes,
        string eventName,
        string causationId);

    string SerializeCloudEvent(CloudEvent cloudEvent);

    /// <summary>
    /// Writes the cache record for the page preceding <paramref name="currentPage"/> once that page is
    /// complete, if it does not exist yet. Call this for every projected feed item.
    /// </summary>
    /// <param name="currentPage">The page the item being projected was written to.</param>
    /// <param name="countCommittedPageItemsAsync">
    /// Returns the number of items on the given page that are <em>committed</em> to the database. It must
    /// not include items that are only tracked as added on the feed context, since publishing a record for
    /// a page whose rows are still pending lets the cache populator cache an incomplete page.
    /// </param>
    /// <remarks>
    /// The feed context is intentionally left untouched: its rows are committed by the projection runner,
    /// together with the projection position, after this returns. Nothing durable may be written ahead of
    /// that position, or the replay after a restart fails on a primary key violation.
    /// </remarks>
    Task MarkCompletedPageAsync(int currentPage, Func<int, Task<int>> countCommittedPageItemsAsync);
}
