namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouweenheid
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De status van een gebouweenheid.
    /// </summary>
    [DataContract(Name = "GebouweenheidStatus", Namespace = "")]
    public enum GebouweenheidStatus
    {
        /// <summary>
        /// Een bouwaanvraag is toegekend voor de gebouweenheid.
        /// </summary>
        [EnumMember]
        Gepland = 1,

        /// <summary>
        /// De gebouweenheid is gerealiseerd (werk afgerond) en is observeerbaar.
        /// </summary>
        [EnumMember]
        Gerealiseerd = 2,

        /// <summary>
        /// De gebouweenheid is gesloopt, gesplitst of samengevoegd.
        /// </summary>
        [EnumMember]
        Gehistoreerd = 3,

        /// <summary>
        /// De bouwaanvraag voor de gebouweenheid is niet toegekend, geannuleerd of verlopen.
        /// </summary>
        [EnumMember]
        NietGerealiseerd = 4
    }
}
