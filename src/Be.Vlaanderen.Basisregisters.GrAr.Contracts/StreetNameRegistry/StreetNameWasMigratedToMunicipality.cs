namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class StreetNameWasMigratedToMunicipality : IMessage
    {
        public string MunicipalityId { get; }

        public string NisCode { get; }

        public string StreetNameId { get; }

        public int PersistentLocalId { get; }

        public string Status { get; }

        public string? PrimaryLanguage { get; }

        public string? SecondaryLanguage { get; }

        public IDictionary<string, string> Names { get; }

        public IDictionary<string, string> HomonymAdditions { get; }

        public bool IsCompleted { get; }

        public bool IsRemoved { get; }

        public Provenance Provenance { get;  }

        public StreetNameWasMigratedToMunicipality(
            string municipalityId,
            string nisCode,
            string streetNameId,
            int persistentLocalId,
            string status,
            string? primaryLanguage,
            string? secondaryLanguage,
            IDictionary<string, string> names,
            IDictionary<string, string> homonymAdditions,
            bool isCompleted,
            bool isRemoved,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            StreetNameId = streetNameId;
            PersistentLocalId = persistentLocalId;
            Status = status;
            PrimaryLanguage = primaryLanguage;
            SecondaryLanguage = secondaryLanguage;
            Names = names;
            HomonymAdditions = homonymAdditions;
            IsCompleted = isCompleted;
            IsRemoved = isRemoved;
            Provenance = provenance;
        }
    }
}
