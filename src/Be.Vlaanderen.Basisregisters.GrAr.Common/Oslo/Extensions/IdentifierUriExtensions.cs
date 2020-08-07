namespace Be.Vlaanderen.Basisregisters.GrAr.Common.Oslo.Extensions
{
    using System;

    public static class IdentifierUriExtensions
    {
        public static IdentifierUri AsIdentifier(this string uri)
            => new IdentifierUri(uri);

        public static IdentifierUri AsIdentifier(this Uri uri)
            => new IdentifierUri(uri);
    }
}
