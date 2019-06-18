namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Een GML3 punt of een GeoJSON punt, afhankelijk van het Content-Type.
    /// </summary>
    [DataContract(Name = "GeometriePolygoon", Namespace = "")]
    public class SyndicationPolygon
    {
        /// <summary>
        /// Een GML3 polygon.
        /// </summary>
        [JsonIgnore]
        [DataMember(Name = "polygon")]
        public GmlPolygon XmlPolygon { get; set; }
    }
}