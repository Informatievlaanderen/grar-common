namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;

    [DataContract(Namespace = "")]
    public class RingProperty
    {
        [DataMember(Name = "LinearRing")]
        public LinearRing LinearRing { get; set; }
    }
}
