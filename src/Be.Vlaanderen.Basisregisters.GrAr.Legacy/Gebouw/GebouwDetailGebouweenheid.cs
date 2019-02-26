namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Gebouweenheid", Namespace = "")]
    public class GebouwDetailGebouweenheid
    {
        /// <summary>
        /// De identifier van de gekoppelde gebouweenheid.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde gebouweenheid weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public string Detail { get; set; }

        public GebouwDetailGebouweenheid(string objectId, string detail)
        {
            ObjectId = objectId;
            Detail = detail;
        }
    }
}