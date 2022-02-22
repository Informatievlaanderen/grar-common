namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System;

    public interface IHasCommandProvenance
    {
        public Provenance Provenance { get; }

        Guid CreateCommandId();
    }
}
