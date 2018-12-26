namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.PostInfo
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Postnaam", Namespace = "")]
    public class Postnaam
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        public GeografischeNaam GeografischeNaam { get; set; }

        public Postnaam(GeografischeNaam geografischeNaam)
        {
            GeografischeNaam = geografischeNaam;
        }
    }
}
