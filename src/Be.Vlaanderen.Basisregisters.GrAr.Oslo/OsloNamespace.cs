namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo;

public readonly record struct OsloNamespace(string Value)
{
    public bool IsEmpty => string.IsNullOrEmpty(Value);

    public override string ToString() => Value;

    public static implicit operator string(OsloNamespace ns) => ns.Value;
    public static implicit operator OsloNamespace(string value) => new(value ?? string.Empty);

    public string ToPuri(string id) => Value.TrimEnd('/') + "/" + id;
}
