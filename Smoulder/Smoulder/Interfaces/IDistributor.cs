using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IDistributor<TDistributeData> : IWorkerUnit
    {
        void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue, ConcurrentQueue<TDistributeData> underlyingQueue);
        int GetDistributorQueueCount();
        bool Dequeue(out TDistributeData item);
        bool Peek(out TDistributeData item);

        void Action(TDistributeData incomingProcessData, CancellationToken cancellationToken);
        void SetAction(Action<TDistributeData, CancellationToken> action);

        void OnEmptyQueue(CancellationToken cancellationToken);
        void SetOnEmptyQueue(Action<CancellationToken> action);
    }
}
