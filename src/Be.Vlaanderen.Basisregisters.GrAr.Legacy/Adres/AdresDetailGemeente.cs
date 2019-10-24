namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;
    using Gemeente;

    /// <summary>
    /// De gemeente die deel uitmaakt van het adres.
    /// </summary>
    [DataContract(Name = "Gemeente", Namespace = "")]
    public class AdresDetailGemeente
    {
        /// <summary>
        /// De identifier van de gekoppelde gemeente.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde gemeente weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public string Detail { get; set; }

        /// <summary>
        /// De naam van de gemeente in het Nederlands.
        /// </summary>
        [DataMember(Name = "Gemeentenaam", Order = 3)]
        public Gemeentenaam Gemeentenaam { get; set; }

        public AdresDetailGemeente(string objectId, string detail, GeografischeNaam geografischeNaam)
        {
            ObjectId = objectId;
            Detail = detail;
            Gemeentenaam = new Gemeentenaam(geografischeNaam);
        }
    }
}
