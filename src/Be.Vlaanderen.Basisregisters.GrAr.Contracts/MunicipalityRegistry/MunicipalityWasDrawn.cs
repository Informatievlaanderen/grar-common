namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using System;
    using Newtonsoft.Json;
    using Provenance;
    using Utilities.HexByteConvertor;

    [EventName("MunicipalityWasDrawn")]
    [EventDescription("De grenzen van de gemeente werden toegevoegd of gewijzigd.")]
    public class MunicipalityWasDrawn : IHasProvenance, ISetProvenance
    {
        [EventPropertyDescription("Interne GUID van de gemeente.")]
        public Guid MunicipalityId { get; }
        
        [EventPropertyDescription("Extended WKB-voorstelling van de gemeentegrenzen.")]
        public string ExtendedWkbGeometry { get; }
        
        [EventPropertyDescription("Metadata bij het event.")]
        public ProvenanceData Provenance { get; private set; }

        public MunicipalityWasDrawn(
            MunicipalityId municipalityId,
            ExtendedWkbGeometry ewkb)
        {
            MunicipalityId = municipalityId;
            ExtendedWkbGeometry = ewkb.ToString();
        }

        [JsonConstructor]
        private MunicipalityWasDrawn(
            Guid municipalityId,
            string extendedWkbGeometry,
            ProvenanceData provenance) :
            this(
                new MunicipalityId(municipalityId),
                new ExtendedWkbGeometry(extendedWkbGeometry.ToByteArray())) => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
