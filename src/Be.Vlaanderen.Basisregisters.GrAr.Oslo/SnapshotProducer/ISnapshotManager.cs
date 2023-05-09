namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using System.Threading;
    using System.Threading.Tasks;
    using NodaTime;

    public interface ISnapshotManager
    {
        Task<OsloResult?> FindMatchingSnapshot(string objectId, Instant eventVersion, string? eventHash, long eventPosition, bool throwStaleWhenGone, CancellationToken ct);
    }
}
