namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouweenheid
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De functie van de gebouweenheid.
    /// </summary>
    [DataContract(Name = "GebouweenheidFunctie", Namespace = "")]
    public enum GebouweenheidFunctie
    {
        /// <summary>
        /// Niet gekend.
        /// </summary>
        [EnumMember]
        NietGekend = 1,

        /// <summary>
        /// Gemeenschappelijk deel.
        /// </summary>
        [EnumMember]
        GemeenschappelijkDeel = 2
    }
}
