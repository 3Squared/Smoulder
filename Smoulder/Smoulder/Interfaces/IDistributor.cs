using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IDistributor<TDistributeData> : IWorkerUnit
    {
        void RegisterDistributorQueue(ConcurrentQueue<TDistributeData> distributorQueue);
        int GetDistributorQueueCount();
        void OnNoQueueItem(CancellationToken cancellationToken);
        TDistributeData Dequeue();
        TDistributeData Peek();
    }
}
