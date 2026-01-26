namespace Be.Vlaanderen.Basisregisters.GrAr.ChangeFeed;

using System.Collections.Generic;
using Newtonsoft.Json;

public class BaseRegistriesCloudEvent
{
    [JsonProperty("@id", Order = 0)]
    public required string Id { get; set; }

    [JsonProperty("objectId", Order = 1)]
    public required string ObjectId { get; set; }

    [JsonProperty("naamruimte", Order = 2)]
    public required string Namespace { get; set; }

    [JsonProperty("versieId", Order = 3)]
    public required string VersionId { get; set; }

    [JsonProperty("nisCodes", Order = 4)]
    public required List<string> NisCodes { get; set; }

    [JsonProperty("attributen", Order = 5)]
    public required List<BaseRegistriesCloudEventAttribute> Attributes { get; set; } = [];
}
