namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Een GML3 punt.
    /// </summary>
    [DataContract(Namespace = "")]
    public class GmlPoint
    {
        [DataMember(Name = "pos")]
        public string Pos { get; set; }
    }
}
