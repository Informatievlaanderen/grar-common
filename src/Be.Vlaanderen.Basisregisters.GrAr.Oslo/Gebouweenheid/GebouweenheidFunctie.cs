namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouweenheid
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// De functie van de gebouweenheid.
    /// </summary>
    public enum GebouweenheidFunctieValue
    {
        /// <summary>
        /// Niet gekend.
        /// </summary>
        NietGekend = 1,

        /// <summary>
        /// Gemeenschappelijk deel.
        /// </summary>
        GemeenschappelijkDeel = 2
    }

    /// <summary>De functie van de gebouweenheid.</summary>
    public class GebouweenheidFunctie
    {
        private static readonly CamelCaseNamingStrategy NamingStrategy = new();

        /// <summary>
        /// Identificatie van de functie.
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// Linked data type van het object.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 2)]
        public string Type => "skos:Concept";

        /// <summary>
        /// De beschrijving van de functie.
        /// </summary>
        [JsonProperty("skos:prefLabel", Required = Required.DisallowNull, Order = 3)]
        public GebouweenheidFunctieValue Label { get; set; }

        public GebouweenheidFunctie(GebouweenheidFunctieValue gebouweenheidFunctie)
        {
            Label = gebouweenheidFunctie;
            Id = OsloNamespaces.GebouweenheidFunctie.ToPuri(NamingStrategy.GetPropertyName(gebouweenheidFunctie.ToString(), false));
        }
    }
}
