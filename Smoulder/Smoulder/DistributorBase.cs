using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class DistributorBase<T> : WorkerUnitBase, IDistributor<T>
    {
        private ConcurrentQueue<T> _distributorQueue;

        public void RegisterDistributorQueue(ConcurrentQueue<T> distributorQueue)
        {
            _distributorQueue = distributorQueue;
        }

        public T Dequeue()
        {
            _distributorQueue.TryDequeue(out var item);
            return item;
        }

        public override void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_distributorQueue.IsEmpty)
                    {
                        OnNoQueueItem(cancellationToken);

                    }
                    else
                    {
                        Action(cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    OnError(e);
                }
            }
        }
    }
}
