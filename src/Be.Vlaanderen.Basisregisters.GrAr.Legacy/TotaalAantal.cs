namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Het totaal aantal.
    /// </summary>
    [DataContract(Name = "TotaalAantal", Namespace = "")]
    public class TotaalAantalResponse
    {
        /// <summary>
        /// Het aantal.
        /// </summary>
        [DataMember(Name = "Aantal", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public int Aantal { get; set; }
    }
}
