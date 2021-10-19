namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Perceel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De status van het perceel.
    /// </summary>
    [DataContract(Name = "PerceelStatus", Namespace = "")]
    public enum PerceelStatus
    {
        /// <summary>
        /// Het perceel is gerealiseerd.
        /// </summary>
        [EnumMember]
        Gerealiseerd = 1,

        /// <summary>
        /// Het perceel is gehistoreerd.
        /// </summary>
        [EnumMember]
        Gehistoreerd = 2,
    }
}
