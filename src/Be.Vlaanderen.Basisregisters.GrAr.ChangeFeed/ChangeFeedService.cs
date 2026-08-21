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

    /// <summary>
    /// Feed pages are numbered from one, so there is never a page to mark while on the first page.
    /// </summary>
    private const int FirstPage = 1;

    private static readonly CloudEventAttribute EventTypeAttribute =
        CloudEventAttribute.CreateExtension(BaseRegistriesCloudEventAttribute.BaseRegistriesEventType, CloudEventAttributeType.String);
    private static readonly CloudEventAttribute CausationIdAttribute =
        CloudEventAttribute.CreateExtension(BaseRegistriesCloudEventAttribute.BaseRegistriesCausationId, CloudEventAttributeType.String);

    public static readonly IReadOnlyList<CloudEventAttribute> ExtensionAttributes =
        [EventTypeAttribute, CausationIdAttribute];

    private readonly ChangeFeedConfig _config;
    private readonly LastChangedListContext _lastChangedListContext;
    private readonly JsonEventFormatter _jsonEventFormatter;

    private readonly Uri _feedSourceUri;
    private readonly Uri _dataSchemaUri;
    private readonly Uri _dataSchemaUriTransform;

    /// <summary>
    /// Highest page already known to have a last changed record, so that the pages that have been marked
    /// during this process are not re-checked for every projected message. Rebuilt from the database on
    /// the first message after a restart.
    /// </summary>
    private int _highestMarkedPage;

    public int MaxPageSize { get; }

    public ChangeFeedService(
        ChangeFeedConfig config,
        LastChangedListContext lastChangedListContext,
        JsonSerializerSettings jsonSerializerSettings,
        int maxPageSize = DefaultMaxPageSize)
    {
        _config = config;
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
        string? objectId,
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

        if (objectId != null)
            cloudEvent.Subject = $"{_config.Namespace}/{objectId}";

        cloudEvent.Validate();
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
            objectId,
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

    public async Task MarkCompletedPageAsync(int currentPage, Func<int, Task<int>> countCommittedPageItemsAsync)
    {
        // Marks the page before the one being written to, never the current one. The previous page's rows
        // were committed by the projection runner in an earlier batch, whereas the current page's rows are
        // still pending in the feed context, so the cache populator can never observe a record for a page
        // that is still incomplete in the database.
        var pageToMark = currentPage - 1;
        if (pageToMark < FirstPage || pageToMark <= _highestMarkedPage)
            return;

        // Counts committed rows only. That is what makes the record safe to publish, so this must not
        // include rows that are merely tracked as added on the feed context.
        if (await countCommittedPageItemsAsync(pageToMark) < MaxPageSize)
            return;

        var id = $"{pageToMark}.{_config.CacheIdSuffix}";

        // Nothing is written to the feed context here. Its rows are committed by the projection runner,
        // together with the projection position, after this method returns. Leaving nothing committed
        // ahead of the position is what keeps the replay after a restart free of primary key violations,
        // and the existence check makes that replay a no-op for a record written before the failure.
        if (!await _lastChangedListContext.LastChangedList.AnyAsync(x => x.Id == id))
        {
            try
            {
                _lastChangedListContext.LastChangedList.Add(new LastChangedRecord
                {
                    AcceptType = "application/cloudevents-batch+json",
                    CacheKey = $"{_config.CacheKeyPrefix}:{pageToMark}",
                    Id = id,
                    Position = pageToMark,
                    LastPopulatedPosition = _config.IsCacheEnabled ? 0 : pageToMark,
                    Uri = $"{_config.CacheLookUpUrl}?page={pageToMark}"
                });
                await _lastChangedListContext.SaveChangesAsync();
            }
            finally
            {
                // This context is long lived, as the service is typically registered as a singleton.
                // Without clearing, a failed save leaves the record tracked as Added and every later save
                // retries it, and successful saves grow the change tracker for the life of the process.
                _lastChangedListContext.ChangeTracker.Clear();
            }
        }

        // Only advanced once the record is known to exist, so a failure above is retried on replay.
        // Keeps the two queries above to roughly one per page instead of one per projected message.
        _highestMarkedPage = pageToMark;
    }
}
