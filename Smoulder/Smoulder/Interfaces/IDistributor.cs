using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IDistributor<TDistributeData> : IWorkerUnit
    {
        void RegisterDistributorQueue(ConcurrentQueue<TDistributeData> distributorQueue);
        void OnNoQueueItem(CancellationToken cancellationToken);
        TDistributeData Dequeue();
    }
}
