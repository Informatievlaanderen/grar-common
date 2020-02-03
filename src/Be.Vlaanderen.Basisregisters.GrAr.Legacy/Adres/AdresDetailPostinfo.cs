namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Een PostInfo object dat deel uitmaakt van het adres.
    /// </summary>
    [DataContract(Name = "PostInfo", Namespace = "")]
    public class AdresDetailPostinfo
    {
        /// <summary>
        /// De identifier van het gekoppelde PostInfo object.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde PostInfo object weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Detail { get; set; }

        public AdresDetailPostinfo(string objectId, string detail)
        {
            ObjectId = objectId;
            Detail = detail;
        }
    }
}
