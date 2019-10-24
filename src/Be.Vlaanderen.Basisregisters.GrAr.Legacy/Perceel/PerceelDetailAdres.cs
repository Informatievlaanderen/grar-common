namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Perceel
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Name = "Adres", Namespace = "")]
    public class PerceelDetailAdres
    {
        /// <summary>
        /// De identifier van het gekoppelde adres.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde adres weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public Uri Detail { get; set; }

        public static PerceelDetailAdres Create(string objectId, Uri detail)
        {
            return new PerceelDetailAdres
            {
                ObjectId = objectId,
                Detail = detail
            };
        }
    }
}
