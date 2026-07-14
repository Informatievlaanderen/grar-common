namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Taal", Namespace = "")]
    public enum Taal
    {
        /// <summary>Nederlands</summary>
        [EnumMember(Value = "nl")]
        Nl,

        /// <summary>Frans</summary>
        [EnumMember(Value = "fr")]
        Fr,

        /// <summary>Duits</summary>
        [EnumMember(Value = "de")]
        De,

        /// <summary>Engels</summary>
        [EnumMember(Value = "en")]
        En
    }
}
