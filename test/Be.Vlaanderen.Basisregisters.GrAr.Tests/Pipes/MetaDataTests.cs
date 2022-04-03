namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Pipes
{
    using System.Collections.Generic;
    using System.Linq;
    using AggregateSource;
    using AggregateSource.Testing;
    using AggregateSource.Testing.Comparers;
    using AggregateSource.Testing.SqlStreamStore.Autofac;
    using Autofac;
    using AutoFixture;
    using CommandHandling;
    using Common.Pipes;
    using EventHandling;
    using EventHandling.Autofac;
    using FluentAssertions;
    using Infrastructure;
    using KellermanSoftware.CompareNetObjects;
    using SqlStreamStore;
    using Xunit;
    using Xunit.Abstractions;

    public class MetadataTests : AutofacBasedTest
    {
        public MetadataTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override void ConfigureCommandHandling(ContainerBuilder builder)
        {
            builder.RegisterType<TestMetadataRepository>().As<ITestMetadataRepository>();

            builder
                .RegisterType<ConcurrentUnitOfWork>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TestMetadataCommandHandlerModule>()
                .Named<CommandHandlerModule>(typeof(TestMetadataCommandHandlerModule).FullName)
                .As<CommandHandlerModule>();

            builder
                .RegisterType<CommandHandlerResolver>()
                .As<ICommandHandlerResolver>();
        }

        protected override void ConfigureEventHandling(ContainerBuilder builder)
        {
            var eventSerializerSettings = EventsJsonSerializerSettingsProvider.CreateSerializerSettings();
            builder.RegisterModule(new EventHandlingModule(typeof(TestMetadataEvent).Assembly, eventSerializerSettings));
        }

        protected override IFactComparer CreateFactComparer()
        {
            var comparer = new CompareLogic();
            return new CompareNetObjectsBasedFactComparer(comparer);
        }

        [Fact]
        public void MetaOnCommandIsAddToAppliedEvents()
        {
            var fixture = new Fixture();
            var command = new TestMetadataCommand(fixture.Create<string>());

            var expectedEvent = new TestMetadataEvent(command.Name);

            var testMetadataId = new TestMetadataId(1);
            Assert(new Scenario()
                .GivenNone()
                .When(command)
                .Then(testMetadataId,
                    expectedEvent));

            var aggregate = Container.Resolve<ITestMetadataRepository>().GetAsync(testMetadataId).GetAwaiter().GetResult();
            aggregate
                .GetChangesWithMetadata()
                .First()
                .Metadata
                .Should()
                .BeEquivalentTo(new Dictionary<string, object>
                {
                    { AddEventHashPipe.HashMetadataKey, expectedEvent.GetHash() }
                });
        }
    }
}
