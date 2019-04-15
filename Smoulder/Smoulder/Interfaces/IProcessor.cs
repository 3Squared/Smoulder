using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IProcessor<TProcessData, TDistributeData> : IWorkerUnit
    {
        void RegisterProcessorQueue(ConcurrentQueue<TProcessData> processorQueue);
        void RegisterDistributorQueue(ConcurrentQueue<TDistributeData> distributorQueue);
        void OnNoQueueItem(CancellationToken cancellationToken);
        TProcessData Dequeue();
        void Enqueue(TDistributeData itemToEnqueue);
    }
}
