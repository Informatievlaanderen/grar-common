namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Straatnaam
{
    using System.Runtime.Serialization;

    [DataContract(Name = "StraatnaamStatus", Namespace = "")]
    public enum StraatnaamStatus
    {
        /// <summary>
        /// Een straatnaam die voorgesteld is.
        /// </summary>
        [EnumMember]
        Voorgesteld = 1,

        /// <summary>
        /// Een straatnaam in gebruik.
        /// </summary>
        [EnumMember]
        InGebruik = 2,

        /// <summary>
        /// Een straatnaam die niet langer in gebruik is.
        /// </summary>
        [EnumMember]
        Gehistoreerd = 3
    }
}
