namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouw
{
    using Newtonsoft.Json;

    public class GebouwBestaatUit
    {
        [JsonProperty("@type", Order = 0, Required = Required.DisallowNull)]
        public string Type => "Gebouweenheid";
    }
}
