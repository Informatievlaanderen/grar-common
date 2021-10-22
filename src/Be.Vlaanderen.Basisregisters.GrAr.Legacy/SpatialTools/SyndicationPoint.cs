namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie van het object in gml- of geoJSON-formaat, afhankelijk van het content-type.
    /// </summary>
    [DataContract(Name = "Positie", Namespace = "")]
    public class SyndicationPoint
    {
        /// <summary>
        /// Een GML3 punt.
        /// </summary>
        [DataMember(Name = "point")]
        [JsonIgnore]
        public GmlPoint XmlPoint { get; set; }
    }
}
