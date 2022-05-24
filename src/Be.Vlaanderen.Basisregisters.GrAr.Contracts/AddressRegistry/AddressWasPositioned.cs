namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasPositioned : IQueueMessage
    {
        public string AddressId { get; }

        public string GeometryMethod { get; }

        public string GeometrySpecification { get; }

        public string ExtendedWkbGeometry { get; }

        public Provenance Provenance { get; }

        public AddressWasPositioned(string addressId,
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
