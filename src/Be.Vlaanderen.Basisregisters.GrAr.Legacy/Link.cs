namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Link naar een gerelateerde resource.
    /// </summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class Link
    {
        /// <summary>
        /// De absolute URL naar de gerelateerde resource.
        /// </summary>
        [DataMember(Name = "href", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public Uri Href { get; set; }
    }
}
