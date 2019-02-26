namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    /// <summary>
    /// De methode waarop de geometrie is verworven.
    /// </summary>
    public enum GeometrieMethode
    {
        /// <summary>
        /// Het object is opgemeten volgens de GRB specificaties.
        /// </summary>
        IngemetenGRB = 1,

        /// <summary>
        /// Het object is geschetst.
        /// </summary>
        Ingeschetst = 2,

        /// <summary>
        /// Het object is opgemeten (genomen van het as-built plan of andere dataset).
        /// </summary>
        Ingemeten = 3
    }
}
