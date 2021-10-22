namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie van het object in gml- of geoJSON-formaat, afhankelijk van het content-type.
    /// </summary>
    [DataContract(Name = "Positie", Namespace = "")]
    public class Point
    {
        /// <summary>
        /// Een GML3 punt.
        /// </summary>
        [DataMember(Name = "point")]
        [JsonIgnore]
        public GmlPoint XmlPoint { get; set; }

        /// <summary>
        /// Een GeoJSON punt.
        /// </summary>
        [JsonProperty("point")] // Do NOT put [DataMember(Name = "point")] here or the XmlDataContractSerializer freaks out
        [XmlIgnore]
        public GeoJSONPoint JsonPoint { get; set; }
    }
}
