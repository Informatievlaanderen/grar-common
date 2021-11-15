namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using System;
    using Newtonsoft.Json;
    using Provenance;

    [EventName("MunicipalityGeometryWasCorrectedToCleared")]
    [EventDescription("De grenzen van de gemeente werden gewist (via correctie).")]
    public class MunicipalityGeometryWasCorrectedToCleared : IHasProvenance, ISetProvenance
    {
        [EventPropertyDescription("Interne GUID van de gemeente.")]
        public Guid MunicipalityId { get; }
        
        [EventPropertyDescription("Metadata bij het event.")]
        public ProvenanceData Provenance { get; private set; }

        public MunicipalityGeometryWasCorrectedToCleared(
            MunicipalityId municipalityId)
        {
            MunicipalityId = municipalityId;
        }

        [JsonConstructor]
        private MunicipalityGeometryWasCorrectedToCleared(
            Guid municipalityId,
            ProvenanceData provenance) :
            this(
                new MunicipalityId(municipalityId)) => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
