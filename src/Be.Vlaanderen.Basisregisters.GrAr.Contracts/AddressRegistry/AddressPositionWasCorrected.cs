namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressPositionWasCorrected : IQueueMessage
    {
        public string AddressId { get; set; }

        public string GeometryMethod { get; set; }

        public string GeometrySpecification { get; set; }

        public string ExtendedWkbGeometry { get; set; }

        public Provenance Provenance { get; }

        public AddressPositionWasCorrected(string addressId,
            string geometryMethod,
            string geometrySpecification,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            AddressId = addressId;
            GeometryMethod = geometryMethod;
            GeometrySpecification = geometrySpecification;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
