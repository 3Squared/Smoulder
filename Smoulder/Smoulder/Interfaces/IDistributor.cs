using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IDistributor<TDistributeData> : IWorkerUnit
    {
        void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue, ConcurrentQueue<TDistributeData> underlyingQueue);
        int GetDistributorQueueCount();
        void Action(TDistributeData incomingProcessData, CancellationToken cancellationToken);
        void OnNoQueueItem(CancellationToken cancellationToken);
        bool Dequeue(out TDistributeData item);
        bool Peek(out TDistributeData item);
    }
}
