namespace Be.Vlaanderen.Basisregisters.GrAr.ChangeFeed;

/// <summary>
/// Config for the <see cref="ChangeFeedService">ChangeFeedService</see>
/// </summary>
public class ChangeFeedConfig
{
    /// <summary>
    /// The namespace used for object identifiers (e.g., "https://data.vlaanderen.be/id/gemeente")
    /// </summary>
    public required string Namespace { get; set; }

    /// <summary>
    /// The source URL for CloudEvents (e.g., "https://api.basisregisters.vlaanderen.be/v2/gemeenten/wijzigingen")
    /// </summary>
    public required string FeedUrl { get; set; }

    /// <summary>
    /// The data schema URL for standard events
    /// </summary>
    public required string DataSchemaUrl { get; set; }

    /// <summary>
    /// The data schema URL for transform events (e.g., merge operations)
    /// </summary>
    public required string DataSchemaUrlTransform { get; set; }

    /// <summary>
    /// The cache key prefix for this feed (e.g., "feed/municipality")
    /// </summary>
    public required string CacheKeyPrefix { get; set; }

    /// <summary>
    /// The relative url for the feed endpoint (e.g., "/v2/gemeenten/wijzigingen") to look up the page and cache
    /// </summary>
    public required string CacheLookUpUrl { get; set; }

    /// <summary>
    /// The identifier prefix for cache records (e.g., "v1.feed")
    /// </summary>
    public required string CacheIdSuffix { get; set; }
}
