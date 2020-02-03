namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Een GML3 punt of een GeoJSON punt, afhankelijk van het Content-Type.
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
        [JsonProperty("point", Required = Required.DisallowNull)] // Do NOT put [DataMember(Name = "point")] here or the XmlDataContractSerializer freaks out
        [XmlIgnore]
        public GeoJSONPoint JsonPoint { get; set; }
    }
}
