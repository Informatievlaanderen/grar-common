namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;

    public class ImportBatchStatus
    {
        public long Id { get; set; }
        public required string ImportFeedId { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset Until { get; set; }
        public required string CrabTimeScope { get; set; }
        public bool Completed { get; set; }
    }
}
