namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Api
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using CommandHandling;

    public interface IIdempotentCommandHandlerModuleProcessor
    {
        Task<CommandMessage> Process(
            dynamic commandToProcess,
            IDictionary<string, object> metadata,
            int currentPosition,
            CancellationToken cancellationToken = default);
    }
}
