namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    using System.Runtime.Serialization;
    using Gebouweenheid;
    using Newtonsoft.Json;

    [DataContract(Name = "Gebouweenheid", Namespace = "")]
    public class GebouwDetailGebouweenheid
    {
        /// <summary>
        /// De objectidentificator van de gekoppelde gebouweenheid.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De status van de gekoppelde gebouweenheid.
        /// </summary>
        [DataMember(Name = "Status", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public GebouweenheidStatus Status { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde gebouweenheid weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 3)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Detail { get; set; }

        public GebouwDetailGebouweenheid(string objectId, GebouweenheidStatus status, string detail)
        {
            ObjectId = objectId;
            Status = status;
            Detail = detail;
        }
    }
}
