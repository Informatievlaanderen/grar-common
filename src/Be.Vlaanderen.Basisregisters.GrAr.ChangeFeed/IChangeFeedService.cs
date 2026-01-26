namespace Be.Vlaanderen.Basisregisters.GrAr.ChangeFeed;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudNative.CloudEvents;
using Microsoft.EntityFrameworkCore;

public interface IChangeFeedService
{
    int MaxPageSize { get; }
    Uri FeedSourceUri { get; }
    Uri DataSchemaUri { get; }
    Uri DataSchemaUriTransform { get; }
    ChangeFeedConfig Config { get; }

    CloudEvent CreateCloudEvent(
        long feedItemId,
        DateTimeOffset timestamp,
        string eventType,
        object data,
        Uri? dataSchema,
        string eventName,
        string causationId);

    CloudEvent CreateCloudEventWithData(
        long feedItemId,
        DateTimeOffset timestamp,
        string eventType,
        string objectId,
        DateTimeOffset versionId,
        List<string> nisCodes,
        List<BaseRegistriesCloudEventAttribute> attributes,
        string eventName,
        string causationId);

    string SerializeCloudEvent(CloudEvent cloudEvent);

    Task CheckToUpdateCacheAsync(int page, DbContext feedContext, Func<int, Task<int>> countPageItemsAsync);
}
