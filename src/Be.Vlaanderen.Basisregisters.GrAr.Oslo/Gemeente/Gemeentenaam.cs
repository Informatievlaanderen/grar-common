namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gemeente
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// De gemeentenaam van de gemeente.
    /// </summary>
    public class Gemeentenaam
    {
        /// <summary>
        /// Het linked-data type van de gemeentenaam.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 1)]
        public string Type => "Gemeentenaam";

        /// <summary>
        /// De gemeentenamen van de gemeente.
        /// </summary>
        [JsonProperty("gemeentenaam", Required = Required.DisallowNull, Order = 2)]
        public required List<GeografischeNaam> Gemeentenamen { get; set; }
    }
}
