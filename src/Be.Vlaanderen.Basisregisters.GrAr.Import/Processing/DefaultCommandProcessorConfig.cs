namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;

    public class DefaultCommandProcessorConfig : ICommandProcessorConfig
    {
        public int NrOfProducers { get; set; } = 10;
        public int BufferSize { get; set; } = 100;
        public int NrOfConsumers { get; set; } = 4;
        public int BatchSize { get; set; } = 500;
        public bool WaitForUserInput { get; } = false;

        public override string ToString() => $"{Environment.NewLine}" +
                                             $"NrOfProducers: {NrOfProducers}{Environment.NewLine}" +
                                             $"BufferSize: {BufferSize}{Environment.NewLine}" +
                                             $"NrOfConsumers: {NrOfConsumers}{Environment.NewLine}" +
                                             $"BatchSize: {BatchSize}"; //{Environment.NewLine}";
    }
}
