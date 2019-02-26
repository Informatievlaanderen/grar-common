namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Perceel", Namespace = "")]
    public class GebouwDetailPerceel
    {
        /// <summary>
        /// De identifier van het gekoppelde perceel.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde perceel weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public string Detail { get; set; }

        public GebouwDetailPerceel(string objectId, string detail)
        {
            ObjectId = objectId;
            Detail = detail;
        }
    }
}