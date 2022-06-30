namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Contracts
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
        [EnumMember(Value = "NietGekend")]
        NietGekend = 1

        //Reserve 2 = Gemeenschappelijk deel (READ only)
    }
}
