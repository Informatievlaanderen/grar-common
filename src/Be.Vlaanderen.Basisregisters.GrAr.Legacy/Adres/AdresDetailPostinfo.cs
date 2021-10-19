namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De postinfo die deel uitmaakt van het adres. 
    /// </summary>
    [DataContract(Name = "PostInfo", Namespace = "")]
    public class AdresDetailPostinfo
    {
        /// <summary>
        /// De objectidentificator van de gekoppelde postinfo. 
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde postinfo weergeeft.
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
