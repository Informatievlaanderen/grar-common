namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;

    public class GmlPoint
    {
        [DataMember(Name = "pos")]
        public string Pos { get; set; }
    }
}
