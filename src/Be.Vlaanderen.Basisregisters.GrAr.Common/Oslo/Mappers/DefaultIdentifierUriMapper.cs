namespace Be.Vlaanderen.Basisregisters.GrAr.Common.Oslo.Mappers
{
    using System;
    using System.Collections.Generic;

    public abstract class DefaultIdentifierUriMapper<T> : IIdentifierUriMapper<T>
    {
        protected abstract IDictionary<IdentifierUri, T> Mapping { get; }

        public T Map(IdentifierUri identifier)
            => Mapping?.ContainsKey(identifier) ?? false
                ? Mapping[identifier]
                : throw new ArgumentException($"No mapping defined for value {identifier}");
    }
}
