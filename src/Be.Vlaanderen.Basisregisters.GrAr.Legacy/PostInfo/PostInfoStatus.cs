namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De status van het PostInfo object.
    /// </summary>
    [DataContract(Name = "PostInfoStatus", Namespace = "")]
    public enum PostInfoStatus
    {
        /// <summary>
        /// Een gerealiseerd object.
        /// </summary>
        [EnumMember]
        Gerealiseerd = 1,

        /// <summary>
        /// Een gehistoreerd object.
        /// </summary>
        [EnumMember]
        Gehistoreerd = 2
    }
}
