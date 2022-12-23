namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using System.Globalization;
    using Common;
    using Newtonsoft.Json;
    
    public sealed class BuildingUnitWasRegularized : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasRegularized(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            Provenance = provenance;
        }
    }
}
