namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using Newtonsoft.Json;

    /// <summary>
    /// Alle posities van de verschillende feeds (waar mogelijk) volgens de gevraagde query.
    /// </summary>
    public class FeedPositieResponse
    {
        /// <summary>
        /// Eventidentificator van de XML/Atom feeds.
        /// </summary>
        [JsonProperty("feed", Order = 1)]
        public long? Feed { get; set; }

        /// <summary>
        /// Het paginanummer van de wijzigingen feed.
        /// </summary>
        [JsonProperty("wijzigingenFeedPagina", Order = 2)]
        public long? WijzigingenFeedPagina { get; set; }

        /// <summary>
        /// De Id binnen de pagina van de wijzigingen feed.
        /// </summary>
        [JsonProperty("wijzigingenFeedId", Order = 3)]
        public long? WijzigingenFeedId { get; set; }
    }
}
