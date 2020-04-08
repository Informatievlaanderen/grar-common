namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api.Messages
{
    using System;

    public class BatchStatusUpdate
    {
        public ImportFeed ImportFeed { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset Until { get; set; }
        public string CrabTimeScope { get; set; }
        public bool Completed { get; set; }
    }
}
