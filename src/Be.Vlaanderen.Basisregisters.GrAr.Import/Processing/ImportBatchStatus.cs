namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using Newtonsoft.Json;

    public class ImportBatchStatus
    {
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public bool Completed { get; set; }

        [JsonIgnore]
        public bool IsInvalid => Until == default;

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
