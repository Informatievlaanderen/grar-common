namespace Be.Vlaanderen.Basisregisters.GrAr.Common.Oslo
{
    using System;
    using System.IO;
    using Mappers;

    public class IdentifierUri
    {
        public Uri Uri { get; }
        public string Value { get; }

        public IdentifierUri(string uri)
            : this(new Uri(uri)) { }

        public IdentifierUri(Uri uri)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            Value = Path.GetFileName(uri.AbsolutePath) ?? string.Empty;
        }

        public IdentifierUri<T> Map<T>(Func<string, T> identifierValueConverter)
            where T : IConvertible => new IdentifierUri<T>(this, identifierValueConverter);

        public IdentifierUri<T> Map<T>(IIdentifierUriMapper<T> mapper)
            => new IdentifierUri<T>(this, mapper);

        public override bool Equals(object obj)
            => Equals(obj as IdentifierUri);

        protected bool Equals(IdentifierUri other)
            => Equals(Uri, other?.Uri);

        public override int GetHashCode()
            => Uri != null ? Uri.GetHashCode() : 0;

        public override string ToString()
            => Uri.AbsolutePath;
    }

    public class IdentifierUri<T> : IdentifierUri
    {
        public new T Value { get; }

        public IdentifierUri(IdentifierUri identifier, Func<string, T> identifierValueConverter)
            : base(identifier.Uri)
        {
            if (identifierValueConverter == null)
                throw new ArgumentNullException(nameof(identifierValueConverter));

            Value = identifierValueConverter(identifier.Value);
        }

        public IdentifierUri(IdentifierUri identifier, IIdentifierUriMapper<T> identifierUriMapper)
            : base(identifier.Uri)
        {
            if (identifierUriMapper == null)
                throw new ArgumentNullException(nameof(identifierUriMapper));

            Value = identifierUriMapper.Map(identifier);
        }

        public static implicit operator T(IdentifierUri<T> identifier)
            => identifier.Value;
    }
}
