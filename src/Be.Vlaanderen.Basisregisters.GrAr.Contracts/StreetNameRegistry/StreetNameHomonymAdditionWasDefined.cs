namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameHomonymAdditionWasDefined : IMessage
    {
        public string StreetNameId { get; }

        public string HomonymAddition { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameHomonymAdditionWasDefined(string streetNameId,
            string homonymAddition,
            string? language,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            HomonymAddition = homonymAddition;
            Language = language;
            Provenance = provenance;
        }
    }
}
