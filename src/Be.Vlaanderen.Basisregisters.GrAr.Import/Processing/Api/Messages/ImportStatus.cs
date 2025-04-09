namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api.Messages
{
    using System;

    public class ImportStatus
    {
        public required string Name { get; set; }
        public ImportStatusBatchScope? LastCompletedBatch { get; set; }
        public ImportStatusBatchScope? CurrentBatch { get; set; }
    }

    public class ImportStatusBatchScope
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset Until { get; set; }
    }
}
