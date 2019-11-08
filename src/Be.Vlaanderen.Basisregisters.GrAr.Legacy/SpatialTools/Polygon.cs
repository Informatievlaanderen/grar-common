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
        /// Een GML3 polygoon.
        /// </summary>
        [DataMember(Name = "polygon")]
        [JsonIgnore]
        public GmlPolygon XmlPolygon { get; set; }

        /// <summary>
        /// Een GeoJSON polygoon.
        /// </summary>
        [JsonProperty("polygon")] // Do NOT put [DataMember(Name = "polygon")] here or the XmlDataContractSerializer freaks out
        [XmlIgnore]
        public GeoJSONPolygon JsonPolygon { get; set; }
    }
}
