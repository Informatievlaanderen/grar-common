namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Straatnaam
{
    using System.Runtime.Serialization;
    using Gemeente;
    using Newtonsoft.Json;

    /// <summary>
    /// De gemeente aan dewelke de straatnaam is toegewezen.
    /// </summary>
    [DataContract(Name = "Gemeente", Namespace = "")]
    public class StraatnaamDetailGemeente
    {
        /// <summary>
        /// De objectidentificator van de gekoppelde gemeente.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde gemeente weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Detail {get; set; }

        /// <summary>
        /// De gemeentenaam in de eerste officiële taal van de gemeente.
        /// </summary>
        [DataMember(Name = "Gemeentenaam", Order = 3)]
        [JsonProperty(Required = Required.DisallowNull)]
        public Gemeentenaam Gemeentenaam { get; set; }
    }
}
