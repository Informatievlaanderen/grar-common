namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using System;
    using Newtonsoft.Json;
    using Provenance;

    [EventTags(EventTag.For.Sync)]
    [EventName("MunicipalityOfficialLanguageWasRemoved")]
    [EventDescription("Een officiële taal van de gemeente werd verwijderd.")]
    public class MunicipalityOfficialLanguageWasRemoved : IHasProvenance, ISetProvenance
    {
        [EventPropertyDescription("Interne GUID van de gemeente.")]
        public Guid MunicipalityId { get; }

        [EventPropertyDescription("Officiële taal van de gemeente. Mogelijkheden: Dutch, French of German.")]
        public Language Language { get; }

        [EventPropertyDescription("Metadata bij het event.")]
        public ProvenanceData Provenance { get; private set; }

        public MunicipalityOfficialLanguageWasRemoved(
            MunicipalityId municipalityId,
            Language language)
        {
            MunicipalityId = municipalityId;
            Language = language;
        }

        [JsonConstructor]
        private MunicipalityOfficialLanguageWasRemoved(
            Guid municipalityId,
            Language language,
            ProvenanceData provenance) :
            this(
                new MunicipalityId(municipalityId),
                language) => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
