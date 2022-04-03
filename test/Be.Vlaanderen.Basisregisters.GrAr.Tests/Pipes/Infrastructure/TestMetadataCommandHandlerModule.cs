namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Pipes.Infrastructure
{
    using System;
    using AggregateSource;
    using CommandHandling;
    using CommandHandling.SqlStreamStore;
    using Common.Pipes;
    using EventHandling;
    using SqlStreamStore;

    public sealed class TestMetadataCommandHandlerModule : CommandHandlerModule
    {
        public TestMetadataCommandHandlerModule(
            Func<ITestMetadataRepository> getRepo,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            Func<IStreamStore> getStreamStore,
            EventMapping eventMapping,
            EventSerializer eventSerializer)
        {
            For<TestMetadataCommand>()
                .AddSqlStreamStore(getStreamStore, getUnitOfWork, eventMapping, eventSerializer)
                .AddEventHash<TestMetadataCommand, TestMetadataAggregate>(getUnitOfWork)
                .Handle(message =>
                {
                    var repo = getRepo();
                    var id = new TestMetadataId(1);
                    var aggregate = new TestMetadataAggregate();
                    aggregate.TestMetadata(message.Command);
                    repo.Add(id, aggregate);
                });
        }
    }
}
