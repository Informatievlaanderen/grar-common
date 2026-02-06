namespace Be.Vlaanderen.Basisregisters.GrAr.ChangeFeed;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectionHandling.LastChangedList;
using ProjectionHandling.LastChangedList.Model;
using Utilities;

public class ChangeFeedService : IChangeFeedService
{
    public const int DefaultMaxPageSize = 100;

    private static readonly CloudEventAttribute EventTypeAttribute =
        CloudEventAttribute.CreateExtension(BaseRegistriesCloudEventAttribute.BaseRegistriesEventType, CloudEventAttributeType.String);
    private static readonly CloudEventAttribute CausationIdAttribute =
        CloudEventAttribute.CreateExtension(BaseRegistriesCloudEventAttribute.BaseRegistriesCausationId, CloudEventAttributeType.String);

    public static readonly IReadOnlyList<CloudEventAttribute> ExtensionAttributes =
        [EventTypeAttribute, CausationIdAttribute];

    private readonly ChangeFeedConfig _config;
    private readonly EventTypeValidation _eventTypeValidation;
    private readonly LastChangedListContext _lastChangedListContext;
    private readonly JsonEventFormatter _jsonEventFormatter;

    private readonly Uri _feedSourceUri;
    private readonly Uri _dataSchemaUri;
    private readonly Uri _dataSchemaUriTransform;

    public int MaxPageSize { get; }

    public ChangeFeedService(
        ChangeFeedConfig config,
        EventTypeValidation eventTypeValidation,
        LastChangedListContext lastChangedListContext,
        JsonSerializerSettings jsonSerializerSettings,
        int maxPageSize = DefaultMaxPageSize)
    {
        _config = config;
        _eventTypeValidation = eventTypeValidation;
        _lastChangedListContext = lastChangedListContext;
        _jsonEventFormatter = new JsonEventFormatter(JsonSerializer.Create(jsonSerializerSettings));

        _feedSourceUri = new Uri(config.FeedUrl);
        _dataSchemaUri = new Uri(config.DataSchemaUrl);
        _dataSchemaUriTransform = new Uri(config.DataSchemaUrlTransform);

        MaxPageSize = maxPageSize;
    }

    public Uri FeedSourceUri => _feedSourceUri;
    public Uri DataSchemaUri => _dataSchemaUri;
    public Uri DataSchemaUriTransform => _dataSchemaUriTransform;
    public ChangeFeedConfig Config => _config;

    public CloudEvent CreateCloudEvent(
        long feedItemId,
        DateTimeOffset timestamp,
        string eventType,
        object data,
        Uri? dataSchema,
        string eventName,
        string causationId)
    {
        var cloudEvent = new CloudEvent(CloudEventsSpecVersion.V1_0, ExtensionAttributes)
        {
            Id = feedItemId.ToString(CultureInfo.InvariantCulture),
            Time = timestamp,
            Type = eventType,
            Source = _feedSourceUri,
            DataContentType = MediaTypeNames.Application.Json,
            Data = data,
            DataSchema = dataSchema ?? _dataSchemaUri,
            [BaseRegistriesCloudEventAttribute.BaseRegistriesEventType] = eventName,
            [BaseRegistriesCloudEventAttribute.BaseRegistriesCausationId] = causationId
        };

        cloudEvent.Validate();
        _eventTypeValidation.Validate(cloudEvent);

        return cloudEvent;
    }

    public CloudEvent CreateCloudEventWithData(
        long feedItemId,
        DateTimeOffset timestamp,
        string eventType,
        string objectId,
        DateTimeOffset versionId,
        List<string> nisCodes,
        List<BaseRegistriesCloudEventAttribute> attributes,
        string eventName,
        string causationId)
    {
        var data = new BaseRegistriesCloudEvent
        {
            Id = $"{_config.Namespace}/{objectId}",
            ObjectId = objectId,
            Namespace = _config.Namespace,
            VersionId = new Rfc3339SerializableDateTimeOffset(versionId).ToString(),
            NisCodes = nisCodes,
            Attributes = attributes
        };

        return CreateCloudEvent(
            feedItemId,
            timestamp,
            eventType,
            data,
            _dataSchemaUri,
            eventName,
            causationId);
    }

    public string SerializeCloudEvent(CloudEvent cloudEvent)
    {
        var bytes = _jsonEventFormatter.EncodeStructuredModeMessage(cloudEvent, out _);
        return Encoding.UTF8.GetString(bytes.Span);
    }

    public async Task CheckToUpdateCacheAsync(int page, DbContext feedContext, Func<int, Task<int>> countPageItemsAsync)
    {
        var pageItemsCount = await countPageItemsAsync(page);
        if (pageItemsCount < (MaxPageSize - 1))
            return;

        // Why save changes?
        await feedContext.SaveChangesAsync();
        await _lastChangedListContext.LastChangedList.AddAsync(new LastChangedRecord
        {
            AcceptType = "application/cloudevents-batch+json",
            CacheKey = $"{_config.CacheKeyPrefix}:{page}",
            Id = $"{page}.{_config.CacheIdSuffix}",
            Position = page,
            Uri = $"{_config.CacheLookUpUrl}?page={page}"
        });
        await _lastChangedListContext.SaveChangesAsync();
    }
}
