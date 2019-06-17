namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api.Messages
{
    using System;

    public class BatchStatusUpdate
    {
        public ImportFeed ImportFeed { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public bool Completed { get; set; }
    }
}
