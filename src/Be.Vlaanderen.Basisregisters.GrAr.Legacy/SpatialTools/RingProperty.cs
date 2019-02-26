namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;

    [DataContract(Namespace = "")]
    public class RingProperty
    {
        public LinearRing LinearRing { get; set; }
    }
}
