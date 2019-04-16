using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IProcessor<TProcessData, TDistributeData> : IWorkerUnit
    {
        void RegisterProcessorQueue(ConcurrentQueue<TProcessData> processorQueue);
        void RegisterDistributorQueue(ConcurrentQueue<TDistributeData> distributorQueue);
        void OnNoQueueItem(CancellationToken cancellationToken);
        void Enqueue(TDistributeData itemToEnqueue);
        void Action(TProcessData incomingProcessData, CancellationToken cancellationToken);
        int GetProcessorQueueCount();
        int GetDistributorQueueCount();
        bool Peek(out TProcessData item);
        bool Dequeue(out TProcessData item);
    }
}
