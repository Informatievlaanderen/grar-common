namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie van het object in gml- of geoJSON-formaat, afhankelijk van het content-type.
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
