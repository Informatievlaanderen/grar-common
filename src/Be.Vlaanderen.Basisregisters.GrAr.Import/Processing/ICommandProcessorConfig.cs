namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    public interface ICommandProcessorConfig
    {
        int NrOfProducers { get; }
        int BufferSize { get; }
        int NrOfConsumers { get; }
        int BatchSize { get; }
        bool WaitForUserInput { get; }
    }
}
