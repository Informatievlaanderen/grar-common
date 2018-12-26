namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    /// <summary>
    /// De status van een adres.
    /// </summary>
    public enum AdresStatus
    {
        /// <summary>
        /// Het adres is voorgesteld, maar is nog niet goedgekeurd.
        /// </summary>
        Voorgesteld = 1,

        /// <summary>
        /// Het adres is formeel goedgekeurd door het gemeentebestuur en is actief in gebruik.
        /// </summary>
        InGebruik = 2,

        /// <summary>
        /// Het adres is formeel gehistoreerd door het gemeentebestuur.
        /// </summary>
        Gehistoreerd = 3
    }
}
