namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Point", Namespace = "")]
    public class GmlPoint
    {
        [DataMember(Name = "Pos")]
        public string Pos { get; set; }
    }
}
