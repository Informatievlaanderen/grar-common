namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouweenheid
{
    /// <summary>
    /// De status van een gebouweenheid.
    /// </summary>
    public enum GebouweenheidStatus
    {
        /// <summary>
        /// Een bouwaanvraag is toegekend voor de gebouweenheid.
        /// </summary>
        Gepland = 1,

        /// <summary>
        /// De gebouweenheid is gerealiseerd (werk afgerond) en is observeerbaar.
        /// </summary>
        Gerealiseerd = 2,

        /// <summary>
        /// De gebouweenheid is gesloopt, gesplitst of samengevoegd.
        /// </summary>
        Gehistoreerd = 3,

        /// <summary>
        /// De bouwaanvraag voor de gebouweenheid is niet toegekend, geannuleerd of verlopen.
        /// </summary>
        NietGerealiseerd = 4
    }
}
