namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gemeente
{
    using System.Runtime.Serialization;

    [DataContract(Name = "GemeenteStatus", Namespace = "")]
    public enum GemeenteStatus
    {
        /// <summary>
        /// Een gemeente in gebruik.
        /// </summary>
        InGebruik = 1,

        /// <summary>
        /// Een gemeente die niet langer in gebruik is.
        /// </summary>
        Gehistoreerd = 2,

        /// <summary>
        /// Een gemeente die voorgesteld is.
        /// </summary>
        Voorgesteld = 3
    }
}
