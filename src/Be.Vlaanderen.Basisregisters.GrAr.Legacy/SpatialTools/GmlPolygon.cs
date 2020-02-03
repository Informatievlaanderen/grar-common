namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Een GML3 polygoon.
    /// </summary>
    [DataContract(Namespace = "")]
    public class GmlPolygon
    {
        [DataMember(Name = "exterior")]
        [JsonProperty(Required = Required.DisallowNull)]
        public RingProperty Exterior { get; set; }

        [DataMember(Name = "interior")]
        [JsonProperty(Required = Required.DisallowNull)]
        public List<RingProperty> Interior { get; set; }
    }
}
