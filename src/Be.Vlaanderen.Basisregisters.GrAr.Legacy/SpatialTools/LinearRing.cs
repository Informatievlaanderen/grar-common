namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract(Namespace = "")]
    public class LinearRing
    {
        [DataMember(Name = "posList")]
        [JsonProperty(Required = Required.DisallowNull)]
        public string PosList { get; set; }
    }
}
