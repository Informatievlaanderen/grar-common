namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance
{
    using System;
    using System.Linq;
    using AggregateSource;
    using AggregateSource.Testing;
    using AggregateSource.Testing.Comparers;
    using AggregateSource.Testing.SqlStreamStore.Autofac;
    using Autofac;
    using CommandHandling;
    using Common.Pipes;
    using DependencyInjection;
    using EventHandling;
    using EventHandling.Autofac;
    using Infrastructure;
    using KellermanSoftware.CompareNetObjects;
    using Microsoft.Extensions.DependencyInjection;
    using SqlStreamStore;
    using Xunit;
    using Xunit.Abstractions;

    public class AutofacMetadataTests : AutofacBasedTest
    {
        public AutofacMetadataTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override void ConfigureCommandHandling(ContainerBuilder builder)
        {
            builder.RegisterType<TestMetadataRepository>().As<ITestMetadataRepository>();

            builder
                .RegisterType<ConcurrentUnitOfWork>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TestMetadataProvenanceFactory>();

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
            var command = new TestMetadaCommand();

            var expectedEvent = new TestMetadataEvent();
            expectedEvent.SetProvenance(
                new TestMetadataProvenanceFactory().CreateFrom(
                    0,
                    false,
                    command.Timestamp,
                    command.Modification,
                    command.Operator,
                    command.Organisation));

            Assert(new Scenario()
                .GivenNone()
                .When(command)
                .Then(new TestMetadataId(1),
                    expectedEvent));
        }
    }

    public class MetadataTests : MicrosoftBasedTest2
    {
        public MetadataTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override void ConfigureCommandHandling(IServiceCollection services)
        {
            services.AddTransient<ITestMetadataRepository, TestMetadataRepository>();
            services.AddTransient<Func<ITestMetadataRepository>>(c => () => new TestMetadataRepository(
                c.GetRequiredService<ConcurrentUnitOfWork>(),
                c.GetRequiredService<IStreamStore>(),
                c.GetRequiredService<EventMapping>(),
                c.GetRequiredService<EventDeserializer>()));

            services.AddScoped<ConcurrentUnitOfWork>();
            services.AddTransient<TestMetadataProvenanceFactory>();
            services.AddTransient<Func<ConcurrentUnitOfWork>>(c => c.GetRequiredService<ConcurrentUnitOfWork>);
            services.AddTransient<CommandHandlerModule, TestMetadataCommandHandlerModule>();
            services.AddTransient<ICommandHandlerResolver>(s => new CommandHandlerResolver(s.GetServices<CommandHandlerModule>().ToArray()));
        }

        protected override void ConfigureEventHandling(IServiceCollection services)
        {
            var eventSerializerSettings = EventsJsonSerializerSettingsProvider.CreateSerializerSettings();
            services.RegisterModules(new EventHandling.Microsoft.EventHandlingModule(typeof(TestMetadataEvent).Assembly, eventSerializerSettings));
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
            expectedEvent.SetProvenance(
                new TestMetadataProvenanceFactory().CreateFrom(
                    0,
                    false,
                    command.Timestamp,
                    command.Modification,
                    command.Operator,
                    command.Organisation));

            Assert(new Scenario()
                .GivenNone()
                .When(command)
                .Then(new TestMetadataId(1),
                    expectedEvent));
        }
    }
}
