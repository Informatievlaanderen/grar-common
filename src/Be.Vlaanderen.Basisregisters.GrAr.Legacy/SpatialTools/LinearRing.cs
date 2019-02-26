namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;

    [DataContract(Namespace = "")]
    public class LinearRing
    {
        [DataMember(Name = "posList")]
        public string PosList { get; set; }
    }
}
