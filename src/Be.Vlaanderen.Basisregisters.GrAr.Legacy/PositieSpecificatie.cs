namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    /// <summary>
    /// De specificatie van het object, voorgesteld door de positie.
    /// </summary>
    public enum PositieSpecificatie
    {
        /// <summary>
        /// De positie duidt een gemeente aan.
        /// </summary>
        Gemeente = 1,

        /// <summary>
        /// De positie duidt een straat aan.
        /// </summary>
        Straat = 2,

        /// <summary>
        /// De positie duidt een perceel aan.
        /// </summary>
        Perceel = 3,

        /// <summary>
        /// De positie duidt een lot aan.
        /// </summary>
        Lot = 4,

        /// <summary>
        /// De positie duidt een standplaats aan.
        /// </summary>
        Standplaats = 5,

        /// <summary>
        /// De positie duidt een ligplaats aan.
        /// </summary>
        Ligplaats = 6,

        /// <summary>
        /// De positie duidt een gebouw aan.
        /// </summary>
        Gebouw = 7,

        /// <summary>
        /// De positie duidt een gebouweenheid aan.
        /// </summary>
        Gebouweenheid = 8,

        /// <summary>
        /// De positie duidt een ingang aan.
        /// </summary>
        Ingang = 9,

        /// <summary>
        /// De positie duidt een wegsegment aan.
        /// </summary>
        Wegsegment = 11
    }
}
