namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouw
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De geometriemethode van het object.
    /// </summary>
    [DataContract(Name = "GebouwGeometrieMethode", Namespace = "")]
    public enum GebouwGeometrieMethode
    {
        /// <summary>
        /// Het object is opgemeten volgens de GRB specificaties.
        /// </summary>
        [EnumMember]
        IngemetenGRB = 1,

        /// <summary>
        /// Het object is geschetst.
        /// </summary>
        [EnumMember]
        Ingeschetst = 2
    }
}
