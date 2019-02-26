namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouw
{
    using System.Runtime.Serialization;

    /// <summary>
    /// De methode waarop de geometrie is verworven.
    /// </summary>
    [DataContract(Name = "GeometrieMethode", Namespace = "")]
    public enum GeometrieMethode
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
        Ingeschetst = 2,

        /// <summary>
        /// Het object is opgemeten (genomen van het as-built plan of andere dataset).
        /// </summary>
        [EnumMember]
        Ingemeten = 3
    }
}
