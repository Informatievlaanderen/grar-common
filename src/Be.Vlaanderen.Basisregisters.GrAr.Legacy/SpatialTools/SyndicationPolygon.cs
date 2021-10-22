namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie van het object in gml- of geoJSON-formaat, afhankelijk van het content-type.
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