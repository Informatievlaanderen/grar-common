namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Link naar een gerelateerde resource.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// De absolute URL naar de gerelateerde resource.
        /// </summary>
        [JsonProperty("href", Order = 1, Required = Required.DisallowNull)]
        public required Uri Href { get; set; }
    }
}
