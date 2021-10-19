namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gemeente
{
    using System.Runtime.Serialization;

    /// <summary>De status van de gemeente.</summary>
    [DataContract(Name = "GemeenteStatus", Namespace = "")]
    public enum GemeenteStatus
    {
        /// <summary>
        /// Een gemeente in gebruik.
        /// </summary>
        [EnumMember]
        InGebruik = 1,

        /// <summary>
        /// Een gemeente die niet langer in gebruik is.
        /// </summary>
        [EnumMember]
        Gehistoreerd = 2,

        /// <summary>
        /// Een gemeente die voorgesteld is.
        /// </summary>
        [EnumMember]
        Voorgesteld = 3
    }
}
