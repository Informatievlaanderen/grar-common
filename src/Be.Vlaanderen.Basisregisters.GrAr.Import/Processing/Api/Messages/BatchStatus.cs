namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api.Messages
{
    using System;

    public class BatchStatus
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset Until { get; set; }
        public bool Completed { get; set; }
    }
}
