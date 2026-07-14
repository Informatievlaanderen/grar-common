namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Adres
{
    using Newtonsoft.Json;

    /// <summary>
    /// Adresvoorstelling in de officiële talen van de gemeente.
    /// </summary>
    public class VolledigAdres
    {
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "Adresuitbreiding";

        /// <summary>
        /// De geografische naam.
        /// </summary>
        [JsonProperty("volledigAdres", Required = Required.DisallowNull, Order = 1)]
        public GeografischeNaam GeografischeNaam { get; set; }

        public VolledigAdres(GeografischeNaam geografischeNaam)
        {
            GeografischeNaam = geografischeNaam;
        }

        public VolledigAdres(
            string straatnaam,
            string huisnummer,
            string busnummer,
            string postcode,
            string gemeentenaam,
            Taal taal)
        {
            var representation = string.IsNullOrEmpty(busnummer) ?
                $"{straatnaam} {huisnummer}, {postcode} {gemeentenaam}" :
                $"{straatnaam} {huisnummer} {TranslateBus(taal)} {busnummer}, {postcode} {gemeentenaam}";

            GeografischeNaam = new GeografischeNaam(representation, taal);
        }

        private static string TranslateBus(Taal taalCode)
        {
            return taalCode switch
            {
                Taal.De => "pf",
                Taal.Fr => "bte",
                _ => "bus"
            };
        }
    }
}
