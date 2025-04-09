namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance.Syndication
{
    using System;
    using System.Runtime.Serialization;
    using Common;
    using GrAr.Provenance;
    using Newtonsoft.Json;
    using NodaTime;
    using Utilities;

    [DataContract(Name = "Creatie", Namespace = "")]
    public class Provenance
    {
        [DataMember(Name = "Tijdstip", Order = 0)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Timestamp { get; set; }

        [DataMember(Name = "Organisatie", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string? Organisation { get; set; }

        [DataMember(Name = "Reden", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Reason { get; set; }

        public Provenance(
            Instant timestamp,
            Organisation? organisation,
            Reason reason)
            : this (
                timestamp.ToBelgianDateTimeOffset(),
                organisation,
                reason)
        { }

        public Provenance(
            DateTimeOffset timestamp,
            Organisation? organisation,
            Reason reason)
        {
            Timestamp = new Rfc3339SerializableDateTimeOffset(timestamp).ToString();
            Organisation = organisation?.ToName();
            Reason = reason;
        }
    }
}
