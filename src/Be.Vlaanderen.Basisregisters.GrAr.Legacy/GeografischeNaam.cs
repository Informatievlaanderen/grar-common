namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;

    [DataContract(Name = "GeografischeNaam", Namespace = "")]
    public class GeografischeNaam
    {
        /// <summary>
        /// De spelling van de geografische naam in de gespecifieerde taal.
        /// </summary>
        [DataMember(Name = "Spelling", Order = 1)]
        public string Spelling { get; set; }

        /// <summary>
        /// De taal van de geografische naam.
        /// </summary>
        [DataMember(Name = "Taal", Order = 2)]
        public Taal Taal { get; set; }

        public GeografischeNaam(string spelling, Taal taalCode)
        {
            Spelling = spelling;
            Taal = taalCode;
        }
    }
}
