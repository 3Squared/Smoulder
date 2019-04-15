using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class DistributorBase<T> : IDistributor<T>
    {
        private ConcurrentQueue<T> _distributorQueue;

        public void RegisterDistributorQueue(ConcurrentQueue<T> distributorQueue)
        {
            _distributorQueue = distributorQueue;
        }

        public int GetDistributorQueueCount()
        {
            return _distributorQueue.Count;
        }

        public T Dequeue()
        {
            _distributorQueue.TryDequeue(out var item);
            return item;
        }

        public void Start(CancellationToken cancellationToken)
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

        public virtual void Startup()
        {
        }

        public virtual void Action(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual void Finalise()
        {
        }

        public virtual void OnError(Exception e)
        {
        }

        public virtual void OnNoQueueItem(CancellationToken cancellationToken)
        {
        }
    }
}
