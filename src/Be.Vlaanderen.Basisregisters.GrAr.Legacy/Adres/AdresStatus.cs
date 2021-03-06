namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Adres
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De status van een adres.
    /// </summary>
    [DataContract(Name = "AdresStatus", Namespace = "")]
    public enum AdresStatus
    {
        /// <summary>
        /// Het adres is voorgesteld, maar is nog niet goedgekeurd.
        /// </summary>
        [EnumMember]
        Voorgesteld = 1,

        /// <summary>
        /// Het adres is formeel goedgekeurd door het gemeentebestuur en is actief in gebruik.
        /// </summary>
        [EnumMember]
        InGebruik = 2,

        /// <summary>
        /// Het adres is formeel gehistoreerd door het gemeentebestuur.
        /// </summary>
        [EnumMember]
        Gehistoreerd = 3
    }
}
