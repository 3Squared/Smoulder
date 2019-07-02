using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IProcessor<TProcessData, TDistributeData> : IWorkerUnit
    {
        void RegisterProcessorQueue(BlockingCollection<TProcessData> processorQueue, ConcurrentQueue<TProcessData> underlyingQueue);
        void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue);
        void Enqueue(TDistributeData itemToEnqueue);
        int GetProcessorQueueCount();
        int GetDistributorQueueCount();
        bool Dequeue(out TProcessData item);
        bool Peek(out TProcessData item);

        TDistributeData Action(TProcessData incomingProcessData, CancellationToken cancellationToken);
        void SetAction(Func<TProcessData, CancellationToken, TDistributeData> action);

        void OnEmptyQueue(CancellationToken cancellationToken);
        void SetOnEmptyQueue(Action<CancellationToken> action);
    }
}
