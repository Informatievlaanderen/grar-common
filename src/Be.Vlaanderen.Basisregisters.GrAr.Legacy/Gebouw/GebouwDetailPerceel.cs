namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract(Name = "Perceel", Namespace = "")]
    public class GebouwDetailPerceel
    {
        /// <summary>
        /// De objectidentificator van het gekoppelde perceel. 
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde perceel weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Detail { get; set; }

        public GebouwDetailPerceel(string objectId, string detail)
        {
            ObjectId = objectId;
            Detail = detail;
        }
    }
}
