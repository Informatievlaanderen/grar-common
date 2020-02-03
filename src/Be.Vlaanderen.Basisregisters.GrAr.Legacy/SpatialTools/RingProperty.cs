namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract(Namespace = "")]
    public class RingProperty
    {
        [DataMember(Name = "LinearRing")]
        [JsonProperty(Required = Required.DisallowNull)]
        public LinearRing LinearRing { get; set; }
    }
}
