namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouweenheid
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De aan het gebouweenheid gekoppelde gebouw.
    /// </summary>
    [DataContract(Name = "Gebouw", Namespace = "")]
    public class GebouweenheidDetailGebouw
    {
        /// <summary>
        /// De objectidentificator van het gekoppelde gebouw.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde gebouw weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Detail { get; set; }

        public GebouweenheidDetailGebouw(string objectId, string detail)
        {
            ObjectId = objectId;
            Detail = detail;
        }
    }
}
