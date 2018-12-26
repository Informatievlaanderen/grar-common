namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// De Geometrie methode van de positie.
    /// </summary>
    public enum PositieGeometrieMethode
    {
        /// <summary>
        /// Aangeduid door de beheerder.
        /// </summary>
        AangeduidDoorBeheerder = 1,

        /// <summary>
        /// Afgeleid van een object.
        /// </summary>
        AfgeleidVanObject = 2,

        /// <summary>
        /// De positie is geïnterpoleerd.
        /// </summary>
        [EnumMember(Value = "Geïnterpoleerd")]
        [XmlEnum(Name = "Geïnterpoleerd")]
        Geinterpoleerd = 3
    }
}
