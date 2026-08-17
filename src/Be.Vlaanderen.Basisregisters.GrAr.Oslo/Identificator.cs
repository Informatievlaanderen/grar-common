namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using System;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Bevat informatie waarmee een object kan geïdentificeerd worden.
    /// </summary>
    public class Identificator
    {
        /// <summary>
        /// Linked data type van het object.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 1)]
        public string Type => "Identificator";

        /// <summary>
        /// Bevat de gestructureerde identificator van het object.
        /// </summary>
        [JsonProperty("gestructureerdeIdentificator", Required = Required.DisallowNull, Order = 2)]
        public GestructureerdeIdentificator GestructureerdeIdentificator { get; set; }

        public Identificator(
            string naamruimte,
            string lokaleIdentificator,
            DateTimeOffset versie)
            : this(
                new GestructureerdeIdentificator(
                    naamruimte,
                    lokaleIdentificator,
                    new Rfc3339SerializableDateTimeOffset(versie).ToString()))
        { }

        public Identificator(
            string naamruimte,
            string lokaleIdentificator,
            string versie)
            : this(
                new GestructureerdeIdentificator(
                    naamruimte,
                    lokaleIdentificator,
                    versie))
        { }

        public Identificator(GestructureerdeIdentificator gestructureerdeIdentificator)
        {
            GestructureerdeIdentificator = gestructureerdeIdentificator;
        }
    }

    /// <summary>
    /// Bevat gestructureerde informatie waarmee een object kan geïdentificeerd worden.
    /// </summary>
    public class GestructureerdeIdentificator
    {
        /// <summary>
        /// Linked data type van het object.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 1)]
        public string Type => "GestructureerdeIdentificator";

        /// <summary>
        /// De naamruimte waarbinnen objecten van een bepaald objecttype geïdentificeerd worden.
        /// </summary>
        [JsonProperty("naamruimte", Required = Required.DisallowNull, Order = 2)]
        public string Naamruimte { get; set; }

        /// <summary>
        /// De lokale identificator (enkel uniek binnen naamruimte).
        /// </summary>
        [JsonProperty("lokaleIdentificator", Required = Required.DisallowNull, Order = 3)]
        public string LokaleIdentificator { get; set; }

        /// <summary>
        /// De versie-identificator (timestamp volgens RFC 3339) (notatie: lokale tijd + verschil t.o.v. UTC).
        /// </summary>
        [JsonProperty("versieIdentificator", Required = Required.DisallowNull, Order = 4)]
        public string VersieIdentificator { get; set; }

        public GestructureerdeIdentificator(
            string naamruimte,
            string lokaleIdentificator,
            DateTimeOffset versie)
            : this(
                naamruimte,
                lokaleIdentificator,
                new Rfc3339SerializableDateTimeOffset(versie).ToString())
        { }

        public GestructureerdeIdentificator(
            string naamruimte,
            string lokaleIdentificator,
            string versie)
        {
            Naamruimte = naamruimte;
            LokaleIdentificator = lokaleIdentificator;
            VersieIdentificator = versie;
        }
    }

    /// <summary>Bevat informatie waarmee de gemeente kan geïdentificeerd worden.</summary>
    public class GemeenteIdentificator : Identificator
    {
        public GemeenteIdentificator(string lokaleIdentificator, DateTimeOffset versie)
            : base(OsloNamespaces.Gemeente, lokaleIdentificator, versie) { }

        public GemeenteIdentificator(string lokaleIdentificator, string versie)
            : base(OsloNamespaces.Gemeente, lokaleIdentificator, versie) { }
    }

    /// <summary>Bevat informatie waarmee de postinfo kan geïdentificeerd worden.</summary>
    public class PostinfoIdentificator : Identificator
    {
        public PostinfoIdentificator(string lokaleIdentificator, DateTimeOffset versie)
            : base(OsloNamespaces.Postinfo, lokaleIdentificator, versie) { }

        public PostinfoIdentificator(string lokaleIdentificator, string versie)
            : base(OsloNamespaces.Postinfo, lokaleIdentificator, versie) { }
    }

    /// <summary>Bevat informatie waarmee de straatnaam kan geïdentificeerd worden.</summary>
    public class StraatnaamIdentificator : Identificator
    {
        public StraatnaamIdentificator(string lokaleIdentificator, DateTimeOffset versie)
            : base(OsloNamespaces.StraatNaam, lokaleIdentificator, versie) { }

        public StraatnaamIdentificator(string lokaleIdentificator, string versie)
            : base(OsloNamespaces.StraatNaam, lokaleIdentificator, versie) { }
    }

    /// <summary>Bevat informatie waarmee het adres kan geïdentificeerd worden.</summary>
    public class AdresIdentificator : Identificator
    {
        public AdresIdentificator(string lokaleIdentificator, DateTimeOffset versie)
            : base(OsloNamespaces.Adres, lokaleIdentificator, versie) { }

        public AdresIdentificator(string lokaleIdentificator, string versie)
            : base(OsloNamespaces.Adres, lokaleIdentificator, versie) { }
    }

    /// <summary>Bevat informatie waarmee het gebouw kan geïdentificeerd worden.</summary>
    public class GebouwIdentificator : Identificator
    {
        public GebouwIdentificator(string lokaleIdentificator, DateTimeOffset versie)
            : base(OsloNamespaces.Gebouw, lokaleIdentificator, versie) { }

        public GebouwIdentificator(string lokaleIdentificator, string versie)
            : base(OsloNamespaces.Gebouw, lokaleIdentificator, versie) { }
    }
    /// <summary>Bevat informatie waarmee de gebouweenheid kan geïdentificeerd worden.</summary>
    public class GebouweenheidIdentificator : Identificator
    {
        public GebouweenheidIdentificator(string lokaleIdentificator, DateTimeOffset versie)
            : base(OsloNamespaces.Gebouweenheid, lokaleIdentificator, versie) { }

        public GebouweenheidIdentificator(string lokaleIdentificator, string versie)
            : base(OsloNamespaces.Gebouweenheid, lokaleIdentificator, versie) { }
    }

    /// <summary>Bevat informatie waarmee het perceel kan geïdentificeerd worden.</summary>
    public class PerceelIdentificator
    {
        /// <summary>
        /// Linked data type van het object.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 1)]
        public string Type => "Identificator";

        /// <summary>
        /// Bevat de gestructureerde identificator van het object.
        /// </summary>
        [JsonProperty("gestructureerdeIdentificator", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore, Order = 2)]
        public GestructureerdeIdentificator? GestructureerdeIdentificator { get; set; }

        /// <summary>
        /// De organisatie die de identificator van het perceel heeft toegekend.
        /// </summary>
        [JsonProperty("toegekendDoor", Required = Required.DisallowNull, Order = 3)]
        public PerceelIdentificatorToegekendDoor ToegekendDoor { get; set; }

        [JsonProperty("identificator", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore, Order = 4)]
        public string? Identificator { get; set; }

        public PerceelIdentificator(
            string lokaleIdentificator,
            DateTimeOffset versie,
            PerceelIdentificatorToegekendDoor toegekendDoor)
            : this(
                new GestructureerdeIdentificator(
                    OsloNamespaces.Perceel,
                    lokaleIdentificator,
                    new Rfc3339SerializableDateTimeOffset(versie).ToString()),
                toegekendDoor)
        { }

        public PerceelIdentificator(
            string lokaleIdentificator,
            string versie,
            PerceelIdentificatorToegekendDoor toegekendDoor)
            : this(
                new GestructureerdeIdentificator(
                    OsloNamespaces.Perceel,
                    lokaleIdentificator,
                    versie),
                toegekendDoor)
        { }

        public PerceelIdentificator(GestructureerdeIdentificator gestructureerdeIdentificator, PerceelIdentificatorToegekendDoor toegekendDoor)
        {
            GestructureerdeIdentificator = gestructureerdeIdentificator;
            ToegekendDoor = toegekendDoor;
        }

        public PerceelIdentificator(string identificator, PerceelIdentificatorToegekendDoor toegekendDoor)
        {
            Identificator = identificator;
            ToegekendDoor = toegekendDoor;
        }
    }

    /// <summary>
    /// De organisatie die de identificator van het perceel heeft toegekend.
    /// </summary>
    public class PerceelIdentificatorToegekendDoor
    {
        /// <summary>
        /// De unieke en persistente identificator van de gekoppelde organisatie (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        public PerceelIdentificatorToegekendDoor(string id)
        {
            Id = id;
        }

        public static PerceelIdentificatorToegekendDoor Basisregisters => new PerceelIdentificatorToegekendDoor("https://data.vlaanderen.be/id/organisatie/OVO054890");
        public static PerceelIdentificatorToegekendDoor Aapd => new PerceelIdentificatorToegekendDoor("https://data.vlaanderen.be/id/organisatie/OVO027170");
    }
}
