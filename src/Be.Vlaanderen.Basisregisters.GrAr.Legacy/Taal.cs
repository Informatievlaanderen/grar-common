namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Taal", Namespace = "")]
    public enum Taal
    {
        /// <summary>Nederlands</summary>
        [EnumMember]
        NL,

        /// <summary>Frans</summary>
        [EnumMember]
        FR,

        /// <summary>Duits</summary>
        [EnumMember]
        DE,

        /// <summary>Engels</summary>
        [EnumMember]
        EN
    }
}
