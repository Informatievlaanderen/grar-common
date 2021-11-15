namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using System;
    using Newtonsoft.Json;
    using Provenance;

    [EventName("MunicipalityGeometryWasCleared")]
    [EventDescription("De grenzen van de gemeente werden gewist.")]
    public class MunicipalityGeometryWasCleared : IHasProvenance, ISetProvenance
    {
        [EventPropertyDescription("Interne GUID van de gemeente.")]
        public Guid MunicipalityId { get; }
        
        [EventPropertyDescription("Metadata bij het event.")]
        public ProvenanceData Provenance { get; private set; }

        public MunicipalityGeometryWasCleared(
            MunicipalityId municipalityId)
        {
            MunicipalityId = municipalityId;
        }

        [JsonConstructor]
        private MunicipalityGeometryWasCleared(
            Guid municipalityId,
            ProvenanceData provenance) :
            this(
                new MunicipalityId(municipalityId)) => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
