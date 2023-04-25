namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingGeometryWasImportedFromGrb : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public long Idn { get; }

        public string VersionDate { get; }

        public string? EndDate { get; }

        public int IdnVersion { get; }

        public string GrbObject { get; }

        public string GrbObjectType { get; }

        public string EventType { get; }

        public string Geometry { get; }

        public decimal? Overlap { get; }

        public Provenance Provenance { get; }

        public BuildingGeometryWasImportedFromGrb(
            int buildingPersistentLocalId,
            long idn,
            string versionDate,
            string? endDate,
            int idnVersion,
            string grbObject,
            string grbObjectType,
            string eventType,
            string geometry,
            decimal? overlap,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            Idn = idn;
            VersionDate = versionDate;
            EndDate = endDate;
            IdnVersion = idnVersion;
            GrbObject = grbObject;
            GrbObjectType = grbObjectType;
            EventType = eventType;
            Geometry = geometry;
            Overlap = overlap;
            Provenance = provenance;
        }
    }
}
