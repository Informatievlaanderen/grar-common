namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De status van een gebouw.
    /// </summary>
    [DataContract(Name = "GebouwStatus", Namespace = "")]
    public enum GebouwStatus
    {
        /// <summary>
        /// Bouwaanvraag is aangevraagd of toegekend voor het gebouw.
        /// </summary>
        [EnumMember]
        Gepland = 1,

        /// <summary>
        /// Het gebouw wordt gebouwd.
        /// </summary>
        [EnumMember]
        InAanbouw = 2,

        /// <summary>
        /// Het gebouw is gerealiseerd of is observeerbaar.
        /// </summary>
        [EnumMember]
        Gerealiseerd = 3,

        /// <summary>
        /// Het gebouw is geslopen, gesplitst of samengevoegd.
        /// </summary>
        [EnumMember]
        Gehistoreerd = 4,

        /// <summary>
        /// De bouwaanvraag is niet aangevraagd, geannuleerd of verlopen.
        /// </summary>
        [EnumMember]
        NietGerealiseerd = 5
    }
}
