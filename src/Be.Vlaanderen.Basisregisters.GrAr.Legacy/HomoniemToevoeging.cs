namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// De homoniemtoevoeging in het Nederlands.
    /// </summary>
    [DataContract(Name = "HomoniemToevoeging", Namespace = "")]
    public class HomoniemToevoeging
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        [JsonProperty(Required = Required.DisallowNull)]
        public GeografischeNaam GeografischeNaam { get; set; }

        public HomoniemToevoeging(GeografischeNaam geografischeNaam)
        {
            GeografischeNaam = geografischeNaam;
        }
    }
}
