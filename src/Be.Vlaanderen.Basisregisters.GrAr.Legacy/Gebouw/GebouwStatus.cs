namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    /// <summary>
    /// De status van een gebouw.
    /// </summary>
    public enum GebouwStatus
    {
        /// <summary>
        /// Bouwaanvraag is aangevraagd of toegekend voor het gebouw.
        /// </summary>
        Gepland = 1,

        /// <summary>
        /// Het gebouw wordt gebouwd.
        /// </summary>
        InAanbouw = 2,

        /// <summary>
        /// Het gebouw is gerealiseerd of is observeerbaar.
        /// </summary>
        Gerealiseerd = 3,

        /// <summary>
        /// Het gebouw is geslopen, gesplitst of samengevoegd.
        /// </summary>
        Gehistoreerd = 4,

        /// <summary>
        /// De bouwaanvraag is niet aangevraagd, geannuleerd of verlopen.
        /// </summary>
        NietGerealiseerd = 5
    }
}
