namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Adres
{
    using Newtonsoft.Json;

    /// <summary>
    /// De status van het adres.
    /// </summary>
    public enum AdresStatus
    {
        /// <summary>
        /// Het adres is voorgesteld, maar is nog niet goedgekeurd.
        /// </summary>
        Voorgesteld = 1,

        /// <summary>
        /// Het adres is formeel goedgekeurd door het gemeentebestuur en is actief in gebruik.
        /// </summary>
        InGebruik = 2,

        /// <summary>
        /// Het adres is formeel gehistoreerd door het gemeentebestuur.
        /// </summary>
        Gehistoreerd = 3,

        /// <summary>
        /// Het adres is formeel afgekeurd door het gemeentebestuur.
        /// </summary>
        Afgekeurd = 4
    }

    /// <summary>
    /// De status van het adres.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Identificatie van de status.
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public required string Id { get; set; }

        /// <summary>
        /// Linked data type van het object.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 2)]
        public string Type => "skos:Concept";

        /// <summary>
        /// De beschrijving van de status.
        /// </summary>
        [JsonProperty("skos:prefLabel", Required = Required.DisallowNull, Order = 3)]
        public required AdresStatus Label { get; set; }
    }
}
