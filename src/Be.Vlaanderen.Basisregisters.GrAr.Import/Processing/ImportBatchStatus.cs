namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;

    public class ImportBatchStatus
    {
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public bool Completed { get; set; }

        public bool IsInvalid => Until == default;
    }
}