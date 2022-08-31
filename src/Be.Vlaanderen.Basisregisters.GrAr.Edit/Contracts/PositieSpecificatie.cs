namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Contracts;

using System.Runtime.Serialization;

/// <summary>
/// De specificatie van de positie.
/// </summary>
[DataContract(Name = "PositieSpecificatie", Namespace = "")]
public enum PositieSpecificatie
{
    /// <summary>
    /// De positie duidt een gemeente aan.
    /// </summary>
    [EnumMember] Gemeente = 1,

    /// <summary>
    /// De positie duidt een perceel aan.
    /// </summary>
    [EnumMember] Perceel = 3,

    /// <summary>
    /// De positie duidt een lot aan.
    /// </summary>
    [EnumMember] Lot = 4,

    /// <summary>
    /// De positie duidt een standplaats aan.
    /// </summary>
    [EnumMember] Standplaats = 5,

    /// <summary>
    /// De positie duidt een ligplaats aan.
    /// </summary>
    [EnumMember] Ligplaats = 6,

    /// <summary>
    /// De positie duidt een ingang aan.
    /// </summary>
    [EnumMember] Ingang = 9
}
