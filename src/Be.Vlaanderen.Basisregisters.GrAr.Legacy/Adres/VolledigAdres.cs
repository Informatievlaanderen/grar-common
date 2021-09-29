namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Adresvoorstelling in de eerste officiële taal van de gemeente.
    /// </summary>
    [DataContract(Name = "VolledigAdres", Namespace = "")]
    public class VolledigAdres
    {
        /// <summary>
        /// De geografische naam.
        /// </summary>
        [DataMember(Name = "GeografischeNaam")]
        [JsonProperty(Required = Required.DisallowNull)]
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
                Taal.DE => "pf",
                Taal.FR => "bte",
                _ => "bus"
            };
        }
    }
}
