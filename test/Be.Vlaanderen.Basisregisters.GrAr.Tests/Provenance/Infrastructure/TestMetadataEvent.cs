namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using EventHandling;
    using GrAr.Provenance;
    using Newtonsoft.Json;

    [EventName("TestMetadataEvent")]
    public class TestMetadataEvent : IHasProvenance, ISetProvenance
    {
        public TestMetadataEvent()
        {
        }
        public ProvenanceData Provenance { get; private set; }
        public void SetProvenance(Provenance provenance)
        {
            Provenance = new ProvenanceData(provenance);
        }

        [JsonConstructor]
        private TestMetadataEvent(ProvenanceData provenance) : this()
        {
            (this as ISetProvenance).SetProvenance(provenance.ToProvenance());
        }
    }
}
