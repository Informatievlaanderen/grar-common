namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
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

        public static implicit operator Point(OsloPoint p) => p.ToPoint();
    }

    public static class PointExtensions
    {
        public static Point ToPoint(this OsloPoint p)
            => new()
            {
                JsonPoint = p.JsonPoint,
                XmlPoint = GmlHelper.ToGmlPoint(p.JsonPoint.Coordinates)
            };

        public static OsloPoint ToOsloPoint(this Point p,
            PositieGeometrieMethode? methode,
            PositieSpecificatie positieSpecificatie)
            => new()
            {
                JsonPoint = p.JsonPoint,
                PositieGeometrieMethode = methode,
                PositieSpecificatie = positieSpecificatie
            };

        public static GmlJSONPoint ToGmlJSONPoint(this GeoJSONPoint p)
            => new() {Coordinates = p.Coordinates, Type = p.Type};

        public static GeoJSONPoint ToGeoJSONPoint(this GmlJSONPoint p)
            => new() {Coordinates = p.Coordinates, Type = p.Type};
    }
}
