namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Een GML3 punt.
    /// </summary>
    [DataContract(Namespace = "")]
    public class GmlPoint
    {
        [DataMember(Name = "pos")]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Pos { get; set; }
    }
}
