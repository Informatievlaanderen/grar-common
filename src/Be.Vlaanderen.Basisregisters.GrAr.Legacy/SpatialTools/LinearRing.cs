namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;

    public class LinearRing
    {
        [DataMember(Name = "posList")]
        public string PosList { get; set; }
    }
}
