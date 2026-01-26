namespace Be.Vlaanderen.Basisregisters.GrAr.ChangeFeed;

using Newtonsoft.Json;

public class BaseRegistriesCloudEventAttribute
{
    public const string BaseRegistriesEventType = "basisregisterseventtype";
    public const string BaseRegistriesCausationId = "basisregisterscausationid";

    [JsonProperty("naam")]
    public string Name { get; set; }

    [JsonProperty("oudeWaarde")]
    public object? OldValue { get; set; }

    [JsonProperty("nieuweWaarde")]
    public object? NewValue { get; set; }

    public BaseRegistriesCloudEventAttribute(string name, object? oldValue, object? newValue)
    {
        Name = name;
        OldValue = oldValue;
        NewValue = newValue;
    }
}
