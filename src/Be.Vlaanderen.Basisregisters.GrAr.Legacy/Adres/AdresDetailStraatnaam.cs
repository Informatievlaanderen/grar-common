namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Straatnaam;

    /// <summary>
    /// De straatnaam die deel uitmaakt van het adres.
    /// </summary>
    [DataContract(Name = "Straatnaam", Namespace = "")]
    public class AdresDetailStraatnaam
    {
        /// <summary>
        /// De objectidentificator van de gekoppelde straatnaam.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde straatnaam weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Detail { get; set; }

        /// <summary>
        /// De straatnaam in de eerste officiële taal van de gemeente.
        /// </summary>
        [DataMember(Name = "Straatnaam", Order = 3)]
        [JsonProperty(Required = Required.DisallowNull)]
        public Straatnaam Straatnaam { get; set; }

        public AdresDetailStraatnaam(string objectId, string detail, GeografischeNaam geografischeNaam)
        {
            ObjectId = objectId;
            Detail = detail;
            Straatnaam = new Straatnaam(geografischeNaam);
        }
    }
}
