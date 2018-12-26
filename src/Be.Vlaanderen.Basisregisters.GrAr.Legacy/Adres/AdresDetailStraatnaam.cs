namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;
    using Straatnaam;

    [DataContract(Name = "Straatnaam", Namespace = "")]
    public class AdresDetailStraatnaam
    {
        /// <summary>
        /// De identifier van de gekoppelde straatnaam.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde straatnaam weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public string Detail { get; set; }

        /// <summary>
        /// De straatnaam in het Nederlands.
        /// </summary>
        [DataMember(Name = "Straatnaam", Order = 3)]
        public Straatnaam Straatnaam { get; set; }

        public AdresDetailStraatnaam(string objectId, string detail, GeografischeNaam geografischeNaam)
        {
            ObjectId = objectId;
            Detail = detail;
            Straatnaam = new Straatnaam(geografischeNaam);
        }
    }
}
