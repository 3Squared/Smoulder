using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class DistributorBase<T> : IDistributor<T> where T : new()
    {
        private ConcurrentQueue<T> _distributorQueue;
        private BlockingCollection<T> _blockingDistributorQueue;
        protected int Timeout = 1000;

        public void RegisterDistributorQueue(ConcurrentQueue<T> distributorQueue)
        {
            _distributorQueue = distributorQueue;
            _blockingDistributorQueue = new BlockingCollection<T>(distributorQueue);
        }

        public int GetDistributorQueueCount()
        {
            return _blockingDistributorQueue.Count;
        }

        public bool Peek(out T item)
        {
            return _distributorQueue.TryPeek(out item);
        }

        public bool Dequeue(out T item)
        {
            return _blockingDistributorQueue.TryTake(out item, Timeout);
        }

        public void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_blockingDistributorQueue.TryTake(out var item, Timeout, cancellationToken))
                    {
                        Action(item, cancellationToken);
                    }
                    else
                    {
                        OnNoQueueItem(cancellationToken);
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

        public virtual void Action(T item, CancellationToken cancellationToken)
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
