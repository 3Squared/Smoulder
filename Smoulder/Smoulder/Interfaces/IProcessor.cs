using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IProcessor<TProcessData, TDistributeData> : IWorkerUnit
    {
        void RegisterProcessorQueue(BlockingCollection<TProcessData> processorQueue, ConcurrentQueue<TProcessData> underlyingQueue);
        void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue);
        void OnNoQueueItem(CancellationToken cancellationToken);
        void Enqueue(TDistributeData itemToEnqueue);
        void Action(TProcessData incomingProcessData, CancellationToken cancellationToken);
        int GetProcessorQueueCount();
        int GetDistributorQueueCount();
        bool Dequeue(out TProcessData item);
        bool Peek(out TProcessData item);
    }
}
