namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using System.Threading;
    using System.Threading.Tasks;
    using NodaTime;

    public interface ISnapshotManager
    {
        Task<OsloResult?> FindMatchingSnapshot(string persistentLocalId, Instant eventVersion, bool throwStaleWhenGone, CancellationToken ct);
    }
}
