namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Perceel
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract(Name = "Adres", Namespace = "")]
    public class PerceelDetailAdres
    {
        /// <summary>
        /// De objectidentificator van het gekoppelde adres. 
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde adres weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public Uri Detail { get; set; }

        public static PerceelDetailAdres Create(
            string objectId,
            Uri detail)
            => new PerceelDetailAdres
            {
                ObjectId = objectId,
                Detail = detail
            };
    }
}
