namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance
{
    using System;
    using AggregateSource;
    using AggregateSource.Testing;
    using AggregateSource.Testing.Comparers;
    using Autofac;
    using CommandHandling;
    using CommandHandling.SqlStreamStore.Autofac;
    using EventHandling;
    using EventHandling.Autofac;
    using Infrastructure;
    using KellermanSoftware.CompareNetObjects;
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
                .RegisterSqlStreamStoreCommandHandler<TestMetadataCommandHandlerModule>(
                    c => handler => new TestMetadataCommandHandlerModule(
                        c.Resolve<Func<ITestMetadataRepository>>(),
                        c.Resolve<Func<ConcurrentUnitOfWork>>(),
                        handler));

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
            var command = new TestMetadaCommand();
            var expectedEvent = new TestMetadataEvent();
            expectedEvent.SetProvenance(new TestMetadataProvenanceFactory().CreateFrom(0, false, command.Timestamp, command.Modification, command.Operator, command.Organisation));
            Assert(new Scenario().GivenNone().When(command).Then(new TestMetadataId(1), expectedEvent));
        }
    }
}
