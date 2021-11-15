namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using System;
    using Newtonsoft.Json;
    using Provenance;

    [EventTags(EventTag.For.Sync)]
    [EventName("MunicipalityWasCorrectedToCurrent")]
    [EventDescription("De gemeente kreeg status 'in gebruik' (via correctie).")]
    public class MunicipalityWasCorrectedToCurrent : IHasProvenance, ISetProvenance
    {
        [EventPropertyDescription("Interne GUID van de gemeente.")]
        public Guid MunicipalityId { get; }

        [EventPropertyDescription("Metadata bij het event.")]
        public ProvenanceData Provenance { get; private set; }

        public MunicipalityWasCorrectedToCurrent(
            MunicipalityId municipalityId)
        {
            MunicipalityId = municipalityId;
        }

        [JsonConstructor]
        private MunicipalityWasCorrectedToCurrent(
            Guid municipalityId,
            ProvenanceData provenance) :
            this(
                new MunicipalityId(municipalityId)) => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
