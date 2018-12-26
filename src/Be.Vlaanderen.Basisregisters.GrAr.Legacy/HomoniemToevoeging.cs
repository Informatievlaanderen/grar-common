namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;

    [DataContract(Name = "HomoniemToevoeging", Namespace = "")]
    public class HomoniemToevoeging
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        public GeografischeNaam GeografischeNaam { get; set; }

        public HomoniemToevoeging(GeografischeNaam geografischeNaam)
        {
            GeografischeNaam = geografischeNaam;
        }
    }
}
