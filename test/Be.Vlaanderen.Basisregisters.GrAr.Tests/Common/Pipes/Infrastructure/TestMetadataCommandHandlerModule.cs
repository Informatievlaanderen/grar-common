namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.Pipes.Infrastructure
{
    using System;
    using AggregateSource;
    using CommandHandling;
    using Be.Vlaanderen.Basisregisters.CommandHandling.SqlStreamStore;
    using EventHandling;
    using GrAr.Common.Pipes;
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
