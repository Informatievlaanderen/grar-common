namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Generate
{
    using System;
    using System.Collections.Generic;

    public interface ICommandGenerator<TKey>
    {
        string Name { get; }

        IEnumerable<TKey> GetChangedKeys(
            DateTime from,
            DateTime until);

        IEnumerable<dynamic> GenerateInitCommandsFor(
            TKey key,
            DateTime from,
            DateTime until);

        IEnumerable<dynamic> GenerateUpdateCommandsFor(
            TKey key,
            DateTime from,
            DateTime until);
    }
}
