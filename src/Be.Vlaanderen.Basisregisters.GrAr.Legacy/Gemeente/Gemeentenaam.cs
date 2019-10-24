namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gemeente
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De naam van de gemeente in het Nederlands.
    /// </summary>
    [DataContract(Name = "Gemeentenaam", Namespace = "")]
    public class Gemeentenaam
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        public GeografischeNaam GeografischeNaam { get; set; }

        public Gemeentenaam(GeografischeNaam geografischeNaam)
            => GeografischeNaam = geografischeNaam;
    }
}
