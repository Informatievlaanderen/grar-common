namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using AggregateSource;

    public interface IProvenanceFactory<in TAggregate>
        where TAggregate : IAggregateRootEntity
    {
        bool CanCreateFrom<TCommand>();

        Provenance CreateFrom(object provenanceHolder,
            TAggregate aggregate);
    }

    public interface IProvenanceFactory
    {
        Provenance Create(
            Reason reason,
            Modification modification);
    }
}
