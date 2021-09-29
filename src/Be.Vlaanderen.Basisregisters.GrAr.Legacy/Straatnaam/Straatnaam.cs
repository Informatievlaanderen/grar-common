namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Straatnaam
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De straatnaam in de eerste officiële taal van de gemeente.
    /// </summary>
    [DataContract(Name = "Straatnaam", Namespace = "")]
    public class Straatnaam
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        [JsonProperty(Required = Required.DisallowNull)]
        public GeografischeNaam GeografischeNaam { get; set; }

        public Straatnaam(GeografischeNaam geografischeNaam)
        {
            GeografischeNaam = geografischeNaam;
        }
    }
}
