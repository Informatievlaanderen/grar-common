namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api.Messages
{
    using System;

    public class BatchStatus
    {
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public bool Completed { get; set; }
    }
}
