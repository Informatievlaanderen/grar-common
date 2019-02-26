namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Een GML3 punt of een GeoJSON punt, afhankelijk van het Content-Type.
    /// </summary>
    [DataContract(Name = "GeometriePolygoon", Namespace = "")]
    public class Polygon
    {
        /// <summary>
        /// Een GML3 polygon.
        /// </summary>
        [JsonIgnore]
        [DataMember(Name = "polygon")]
        public GmlPolygon XmlPolygon { get; set; }

        /// <summary>
        /// Een GeoJSON polygon.
        /// </summary>
        [XmlIgnore]
        [DataMember(Name = "polygon")]
        public GeoJSONPolygon JsonPolygon { get; set; }
    }
}
