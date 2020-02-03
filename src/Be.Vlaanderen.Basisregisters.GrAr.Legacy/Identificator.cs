namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System;
    using System.Runtime.Serialization;
    using System.Web;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Bevat informatie waarmee een object kan geïdentificeerd worden.
    /// </summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class Identificator
    {
        /// <summary>
        /// De unieke en persistente identificator van het object (volgt de Vlaamse URI-standaard).
        /// </summary>
        [DataMember(Name = "Id", Order = 1)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Id { get; set; }

        /// <summary>
        /// De naamruimte waarbinnen objecten van een bepaald objecttype geïdentificeerd worden.
        /// </summary>
        [DataMember(Name = "Naamruimte", Order = 2)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Naamruimte { get; set; }

        /// <summary>
        /// De objectidentificator (enkel uniek binnen naamruimte).
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 3)]
        [JsonProperty(Required = Required.DisallowNull)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De versie-identificator (timestamp volgens RFC 3339) (notatie: lokale tijd + verschil t.o.v. UTC).
        /// </summary>
        [DataMember(Name = "VersieId", Order = 4)]
        [JsonProperty(Required = Required.DisallowNull)]
        public Rfc3339SerializableDateTimeOffset Versie { get; set; }

        public Identificator(
            string naamruimte,
            string objectId,
            DateTimeOffset? versie)
        {
            Naamruimte = naamruimte;
            Id = $"{naamruimte}/{HttpUtility.UrlEncode(objectId)}";
            ObjectId = objectId;
            Versie = versie ?? new Rfc3339SerializableDateTimeOffset(DateTimeOffset.MinValue);
        }
    }

    /// <summary>Bevat informatie waarmee een gemeente kan geïdentificeerd worden.</summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class GemeenteIdentificator : Identificator
    {
        public GemeenteIdentificator(string naamruimte, string objectId, DateTimeOffset? versie)
            : base(naamruimte, objectId, versie) { }
    }

    /// <summary>Bevat informatie waarmee de postinfo kan geïdentificeerd worden.</summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class PostinfoIdentificator : Identificator
    {
        public PostinfoIdentificator(string naamruimte, string objectId, DateTimeOffset? versie)
            : base(naamruimte, objectId, versie) { }
    }

    /// <summary>Bevat informatie waarmee de straatnaam kan geïdentificeerd worden.</summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class StraatnaamIdentificator : Identificator
    {
        public StraatnaamIdentificator(string naamruimte, string objectId, DateTimeOffset? versie)
            : base(naamruimte, objectId, versie) { }
    }

    /// <summary>Bevat informatie waarmee het adres kan geïdentificeerd worden.</summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class AdresIdentificator : Identificator
    {
        public AdresIdentificator(string naamruimte, string objectId, DateTimeOffset? versie)
            : base(naamruimte, objectId, versie) { }
    }

    /// <summary>Bevat informatie waarmee het gebouw kan geïdentificeerd worden.</summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class GebouwIdentificator : Identificator
    {
        public GebouwIdentificator(string naamruimte, string objectId, DateTimeOffset? versie)
            : base(naamruimte, objectId, versie) { }
    }
    /// <summary>Bevat informatie waarmee de gebouweenheid kan geïdentificeerd worden.</summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class GebouweenheidIdentificator : Identificator
    {
        public GebouweenheidIdentificator(string naamruimte, string objectId, DateTimeOffset? versie)
            : base(naamruimte, objectId, versie) { }
    }

    /// <summary>Bevat informatie waarmee het perceel kan geïdentificeerd worden.</summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class PerceelIdentificator : Identificator
    {
        public PerceelIdentificator(string naamruimte, string objectId, DateTimeOffset? versie)
            : base(naamruimte, objectId, versie) { }
    }
}
