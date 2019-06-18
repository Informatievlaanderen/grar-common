namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Een GML3 punt of een GeoJSON punt, afhankelijk van het Content-Type.
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
