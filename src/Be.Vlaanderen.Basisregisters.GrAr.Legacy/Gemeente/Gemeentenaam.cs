namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gemeente
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De gemeentenaam in de eerste officiële taal van de gemeente.
    /// </summary>
    [DataContract(Name = "Gemeentenaam", Namespace = "")]
    public class Gemeentenaam
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        [JsonProperty(Required = Required.DisallowNull)]
        public GeografischeNaam GeografischeNaam { get; set; }

        public Gemeentenaam(GeografischeNaam geografischeNaam)
            => GeografischeNaam = geografischeNaam;
    }
}
