namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Straatnaam
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Straatnaam", Namespace = "")]
    public class Straatnaam
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        public GeografischeNaam GeografischeNaam { get; set; }

        public Straatnaam(GeografischeNaam geografischeNaam)
        {
            GeografischeNaam = geografischeNaam;
        }
    }
}
