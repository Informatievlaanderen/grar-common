namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.Pipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autofac;
    using AutoFixture;
    using AggregateSource;
    using AggregateSource.SqlStreamStore.Microsoft;
    using AggregateSource.Testing;
    using AggregateSource.Testing.CommandHandling;
    using AggregateSource.Testing.Comparers;
    using AggregateSource.Testing.SqlStreamStore;
    using AggregateSource.Testing.SqlStreamStore.Microsoft;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing.SqlStreamStore.Autofac;
    using CommandHandling;
    using EventHandling;
    using Be.Vlaanderen.Basisregisters.EventHandling.Autofac;
    using DependencyInjection;
    using FluentAssertions;
    using GrAr.Common.Pipes;
    using Infrastructure;
    using KellermanSoftware.CompareNetObjects;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
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
    public class MetadataTests : MicrosoftBasedTest2
    {
        public MetadataTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, (services) =>
        {

        })
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
            var fixture = new Fixture();
            var command = new TestMetadataCommand(fixture.Create<string>());

            var expectedEvent = new TestMetadataEvent(command.Name);

            var testMetadataId = new TestMetadataId(1);
            Assert(new Scenario()
                .GivenNone()
                .When(command)
                .Then(testMetadataId,
                    expectedEvent));

            var aggregate = Container.GetRequiredService<ITestMetadataRepository>().GetAsync(testMetadataId).GetAwaiter().GetResult();
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

    public abstract class MicrosoftBasedTest2
    {
        private readonly Lazy<IServiceProvider> _serviceProvider;

        protected IServiceProvider Container => _serviceProvider.Value;

        protected IExceptionCentricTestSpecificationRunner ExceptionCentricTestSpecificationRunner => _serviceProvider.Value.GetRequiredService<IExceptionCentricTestSpecificationRunner>();

        protected IEventCentricTestSpecificationRunner EventCentricTestSpecificationRunner => _serviceProvider.Value.GetRequiredService<IEventCentricTestSpecificationRunner>();

        protected IFactComparer FactComparer => _serviceProvider.Value.GetRequiredService<IFactComparer>();

        protected IExceptionComparer ExceptionComparer => _serviceProvider.Value.GetRequiredService<IExceptionComparer>();

        protected ILogger Logger => _serviceProvider.Value.GetRequiredService<ILogger>();

        protected MicrosoftBasedTest2(
            ITestOutputHelper testOutputHelper,
            Action<IServiceCollection> registerFunc = null)
        {
            _serviceProvider = new Lazy<IServiceProvider>(() =>
            {
                var serviceCollection = new ServiceCollection();

                ConfigureEventHandling(serviceCollection);
                ConfigureCommandHandling(serviceCollection);
                serviceCollection.RegisterModule(new SqlStreamStoreModule());

                serviceCollection.UseAggregateSourceTesting(CreateFactComparer(), CreateExceptionComparer());

                serviceCollection.AddTransient(_ => testOutputHelper);
                serviceCollection.AddTransient<ILogger, XUnitLogger>();

                registerFunc?.Invoke(serviceCollection);

                serviceCollection.AddTransient<IExceptionCentricTestSpecificationRunner, ExceptionCentricTestSpecificationRunner>();

                serviceCollection.AddTransient<Func<IStreamStore>>(c => c.GetRequiredService<IStreamStore>);

                serviceCollection.AddTransient<IEventCentricTestSpecificationRunner, EventCentricTestSpecificationRunner>();
                serviceCollection.AddTransient<IFactWriter, StreamStoreFactRepository>();
                serviceCollection.AddTransient<IFactReader, StreamStoreFactRepository>();
                serviceCollection.AddTransient<IHandlerResolver, ReflectionBasedHandlerResolver>();

                return serviceCollection.BuildServiceProvider();
            });
        }

        protected abstract void ConfigureCommandHandling(IServiceCollection services);
        protected abstract void ConfigureEventHandling(IServiceCollection services);

        protected virtual IFactComparer CreateFactComparer()
        {
            var comparer = new CompareLogic();
            return new CompareNetObjectsBasedFactComparer(comparer);
        }

        protected virtual IExceptionComparer CreateExceptionComparer()
        {
            var comparer = new CompareLogic();
            comparer.Config.MembersToIgnore.Add("Source");
            comparer.Config.MembersToIgnore.Add("StackTrace");
            comparer.Config.MembersToIgnore.Add("TargetSite");
            return new CompareNetObjectsBasedExceptionComparer(comparer);
        }

        protected void Assert(IExceptionCentricTestSpecificationBuilder builder)
            => builder.Assert(ExceptionCentricTestSpecificationRunner, ExceptionComparer, Logger);

        protected void Assert(IEventCentricTestSpecificationBuilder builder)
            => builder.Assert(EventCentricTestSpecificationRunner, FactComparer, Logger);
    }
}
