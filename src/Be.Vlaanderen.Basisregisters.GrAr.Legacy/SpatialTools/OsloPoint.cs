namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie van het object in geoJSON-formaat.
    /// </summary>
    [DataContract(Name = "Positie", Namespace = "")]
    public class OsloPoint
    {
        /// <summary>
        /// Een GeoJSON punt.
        /// </summary>
        [JsonProperty("point")] // Do NOT put [DataMember(Name = "point")] here or the XmlDataContractSerializer freaks out
        [XmlIgnore]
        public GmlJSONPoint JsonPoint { get; set; }

        /// <summary>
        /// De gebruikte methode om de positie te bepalen.
        /// </summary>
        [DataMember(Name = "PositieGeometrieMethode", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public PositieGeometrieMethode? PositieGeometrieMethode { get; set; }

        /// <summary>
        /// De specificatie van het object, voorgesteld door de positie.
        /// </summary>
        [DataMember(Name = "PositieSpecificatie", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public PositieSpecificatie PositieSpecificatie { get; set; }

        public OsloPoint()
        {
        }

        public OsloPoint(string gml)
        {
            JsonPoint = new GmlJSONPoint(gml);
        }
    }

    public static class PointExtensions
    {
        public static Point ToPoint(this OsloPoint p,
            double[] coordinates,
            GmlPoint xmlPoint)
            => new()
            {
                JsonPoint = p.JsonPoint.ToGeoJSONPoint(coordinates),
                XmlPoint =  xmlPoint
            };

        public static OsloPoint ToOsloPoint(this Point p,
            string gml,
            PositieGeometrieMethode? methode,
            PositieSpecificatie positieSpecificatie)
            => new()
            {
                JsonPoint = p.JsonPoint.ToGmlJSONPoint(gml),
                PositieGeometrieMethode = methode,
                PositieSpecificatie = positieSpecificatie
            };

        public static GmlJSONPoint ToGmlJSONPoint(this GeoJSONPoint p, string gml)
            => new() {Type = p.Type, Gml = gml};

        public static GeoJSONPoint ToGeoJSONPoint(this GmlJSONPoint p, double[] coordinates)
            => new() {Coordinates = coordinates, Type = p.Type};
    }
}
