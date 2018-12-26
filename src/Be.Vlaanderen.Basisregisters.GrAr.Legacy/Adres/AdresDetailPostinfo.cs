namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;

    [DataContract(Name = "PostInfo", Namespace = "")]
    public class AdresDetailPostinfo
    {
        /// <summary>
        /// De identifier van het gekoppelde PostInfo object.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde PostInfo object weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public string Detail { get; set; }

        public AdresDetailPostinfo(string objectId, string detail)
        {
            ObjectId = objectId;
            Detail = detail;
        }
    }
}
