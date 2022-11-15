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
        NietGekend = 1,

        //Reserve 2 = Gemeenschappelijk deel (READ only)

        /// <summary>
        /// Wonen.
        /// </summary>
        [EnumMember(Value = "Wonen")]
        Wonen = 3,

        /// <summary>
        /// Verblijfsrecreatie.
        /// </summary>
        [EnumMember(Value = "Verblijfsrecreatie")]
        Verblijfsrecreatie = 4,

        /// <summary>
        /// Dagrecreatie, met inbegrip van sport.
        /// </summary>
        [EnumMember(Value = "Dagrecreatie")]
        Dagrecreatie = 5,

        /// <summary>
        /// Land- en tuinbouw in de ruime zin.
        /// </summary>
        [EnumMember(Value = "LandEnTuinbouw")]
        LandEnTuinbouw = 6,

        /// <summary>
        /// Detailhandel.
        /// </summary>
        [EnumMember(Value = "Detailhandel")]
        Detailhandel = 7,

        /// <summary>
        /// Dancing, restaurant en café.
        /// </summary>
        [EnumMember(Value = "DancingRestaurantEnCafe")]
        DancingRestaurantEnCafe = 8,

        /// <summary>
        /// Kantoorfunctie, dienstverlening en vrije beroepen.
        /// </summary>
        [EnumMember(Value = "KantoorfunctieDienstverleningEnVrijeBeroepen")]
        KantoorfunctieDienstverleningEnVrijeBeroepen = 9,

        /// <summary>
        /// Industrie en bedrijvigheid.
        /// </summary>
        [EnumMember(Value = "IndustrieEnBedrijvigheid")]
        IndustrieEnBedrijvigheid = 10,

        /// <summary>
        /// Gemeenschapsvoorzieningen en openbare nutsvoorzieningen.
        /// </summary>
        [EnumMember(Value = "GemeenschapsvoorzieningenEnOpenbareNutsvoorzieningen")]
        GemeenschapsvoorzieningenEnOpenbareNutsvoorzieningen = 11,

        /// <summary>
        /// Militaire functie.
        /// </summary>
        [EnumMember(Value = "Militair")]
        Militair = 12,
    }
}
