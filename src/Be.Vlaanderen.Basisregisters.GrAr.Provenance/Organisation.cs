namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System.Collections.Generic;

    public enum Organisation
    {
        Unknown = 0,
        Municipality = 1,
        NationalRegister = 2,
        Akred = 3,
        TeleAtlas = 4,
        Vlm = 5,
        Ngi = 6,
        DePost = 7,
        NavTeq = 8,
        Vkbo = 9,
        Agiv = 10,
        Aiv = 11,
        Other = 12,
        DigitaalVlaanderen = 13,
    }

    public static class OrganisationExtensions
    {
        private static readonly IDictionary<Organisation, string> Names = new Dictionary<Organisation, string>
        {
            {Organisation.Unknown, "Onbekend"},
            {Organisation.Municipality, "Gemeente"},
            {Organisation.NationalRegister, "Federale Overheidsdienst Binnenlandse Zaken (Rijksregister)"},
            {Organisation.Akred, "Federale Overheidsdienst Financiën (Algemene Administratie van de Patrimoniumdocumentatie)"},
            {Organisation.TeleAtlas, "TeleAtlas"},
            {Organisation.Vlm, "Vlaamse Landmaatschappij"},
            {Organisation.Ngi, "Nationaal Geografisch Instituut"},
            {Organisation.DePost, "bpost"},
            {Organisation.NavTeq, "NAVTEQ"},
            {Organisation.Vkbo, "Coördinatiecel Vlaams e-government"},
            {Organisation.Agiv, "Agentschap voor Geografische Informatie Vlaanderen"},
            {Organisation.Aiv, "Agentschap Informatie Vlaanderen"},
            {Organisation.Other, "Andere"},
            {Organisation.DigitaalVlaanderen, "Digitaal Vlaanderen"}
        };

        public static string ToName(this Organisation organisation) => Names[organisation];
    }
}
