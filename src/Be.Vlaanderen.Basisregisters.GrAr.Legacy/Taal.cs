namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Taal", Namespace = "")]
    public enum Taal
    {
        /// <summary>Nederlands</summary>
        NL,

        /// <summary>Frans</summary>
        FR,

        /// <summary>Duits</summary>
        DE,

        /// <summary>Engels</summary>
        EN
    }
}
