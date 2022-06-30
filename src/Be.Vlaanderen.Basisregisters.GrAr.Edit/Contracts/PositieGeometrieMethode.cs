namespace Be.Vlaanderen.Basisregisters.GrAr.Edit
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
        AfgeleidVanObject = 2

        //Reserve 3 = Interpolated (READ only)
    }
}
