namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Taal", Namespace = "")]
    public enum Taal
    {
        /// <summary>Nederlands</summary>
        [EnumMember(Value = "nl")]
        NL,

        /// <summary>Frans</summary>
        [EnumMember(Value = "fr")]
        FR,

        /// <summary>Duits</summary>
        [EnumMember(Value = "de")]
        DE,

        /// <summary>Engels</summary>
        [EnumMember(Value = "en")]
        EN
    }
}
