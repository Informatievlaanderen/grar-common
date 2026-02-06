namespace Be.Vlaanderen.Basisregisters.GrAr.ChangeFeed;

using System;
using System.Collections.Generic;
using System.Linq;
using CloudNative.CloudEvents;

public class EventTypeValidation(IDictionary<string, string[]> acceptedMapping)
{
    public bool Validate(CloudEvent cloudEvent)
    {
        var cloudEventType = cloudEvent.Type;

        if (string.IsNullOrWhiteSpace(cloudEventType))
            throw new ArgumentNullException(nameof(CloudEvent.Type));

        var baseRegistriesEventType = cloudEvent.GetAttribute(BaseRegistriesCloudEventAttribute.BaseRegistriesEventType);

        if (baseRegistriesEventType is null)
            // Should be a correction!
            return cloudEventType.Contains("correction", StringComparison.InvariantCultureIgnoreCase);

        if (!acceptedMapping.TryGetValue(baseRegistriesEventType.Name, out var acceptedEventTypes))
            throw new ArgumentException($"No known mapping for {baseRegistriesEventType.Name}");

        // Does case sensitivity matter?
        // It's a list because readdress and municipality merger events lead to both a create or update and a transform cloud event.
        return acceptedEventTypes.Contains(cloudEventType, StringComparer.InvariantCultureIgnoreCase);
    }
}
