namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Alle posities van de verschillende feeds (waar mogelijk) volgens de gevraagde query.
    /// </summary>
    [DataContract(Name = "FeedPosities", Namespace = "")]
    public class FeedPositieResponse
    {
        /// <summary>
        /// Eventidentificator van de XML/Atom feeds.
        /// </summary>
        [DataMember(Order = 1)]
        public long? Feed { get; set; }

        /// <summary>
        /// Het paginanummer van de wijzigingen feed.
        /// </summary>
        [DataMember(Order = 2)]
        public long? WijzigingenFeedPagina { get; set; }

        /// <summary>
        /// De Id binnen de pagina van de wijzigingen feed.
        /// </summary>
        [DataMember(Order = 3)]
        public long? WijzigingenFeedId { get; set; }
    }
}
