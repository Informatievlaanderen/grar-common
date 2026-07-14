namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De geometriemethode van de positie.
    /// </summary>
    [DataContract(Name = "PositieGeometrieMethode", Namespace = "")]
    public enum PositieGeometrieMethode
    {
        /// <summary>
        /// Aangeduid door de beheerder.
        /// </summary>
        [EnumMember(Value = "AangeduidDoorBeheerder")]
        AangeduidDoorBeheerder = 1,

        /// <summary>
        /// Afgeleid van een object.
        /// </summary>
        [EnumMember(Value = "AfgeleidVanObject")]
        AfgeleidVanObject = 2,

        /// <summary>
        /// De positie is geïnterpoleerd.
        /// </summary>
        [EnumMember(Value = "Geïnterpoleerd")]
        Geinterpoleerd = 3
    }
}
