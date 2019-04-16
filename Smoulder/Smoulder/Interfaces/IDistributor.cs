using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IDistributor<TDistributeData> : IWorkerUnit
    {
        void RegisterDistributorQueue(ConcurrentQueue<TDistributeData> distributorQueue);
        int GetDistributorQueueCount();
        void Action(TDistributeData incomingProcessData, CancellationToken cancellationToken);
        void OnNoQueueItem(CancellationToken cancellationToken);
        bool Peek(out TDistributeData item);
        bool Dequeue(out TDistributeData item);
    }
}
