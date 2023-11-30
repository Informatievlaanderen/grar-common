namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie van het object in gml- of geoJSON-formaat, afhankelijk van het content-type.
    /// </summary>
    [DataContract(Name = "MeervoudigeOppervlakte", Namespace = "")]
    public class SyndicationMultiSurface
    {
        /// <summary>
        /// Een GML3 MultiSurface.
        /// </summary>
        [JsonIgnore]
        [DataMember(Name = "multiSurface")]
        public GmlMultiSurface XmlMultiSurface { get; set; }
    }

    /// <summary>
    /// Een GML3 MultiSurface.
    /// </summary>
    [CollectionDataContract(ItemName = "surfaceMember", Namespace = "")]
    public class GmlMultiSurface : List<GmlSurfaceMember>
    {
        public GmlMultiSurface()
        { }
        public GmlMultiSurface(List<GmlSurfaceMember> collection) : base (collection)
        { }
    }

    /// <summary>
    /// Een GML3 SurfaceMember.
    /// </summary>
    [DataContract(Name="surfaceMember", Namespace = "")]
    public class GmlSurfaceMember
    {
        /// <summary>
        /// Een GML3 polygon.
        /// </summary>
        [JsonIgnore]
        [DataMember(Name = "polygon")]
        public GmlPolygon Polygon { get; set; }
    }
}
