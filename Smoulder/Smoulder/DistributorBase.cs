using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class DistributorBase<TDistributeData> : IDistributor<TDistributeData> where TDistributeData : new()
    {
        private BlockingCollection<TDistributeData> _distributorQueue;
        private ConcurrentQueue<TDistributeData> _underlyingQueue;
        protected int Timeout = 1000;

        public void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue, ConcurrentQueue<TDistributeData> underlyingQueue)
        {
            _distributorQueue = distributorQueue;
            _underlyingQueue = underlyingQueue;
        }

        public int GetDistributorQueueCount()
        {
            return _distributorQueue.Count;
        }

        public bool Dequeue(out TDistributeData item)
        {
            return _distributorQueue.TryTake(out item, Timeout);
        }

        public bool Peek(out TDistributeData item)
        {
            return _underlyingQueue.TryPeek(out item);
        }

        public void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_distributorQueue.TryTake(out var item, Timeout, cancellationToken))
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

        public virtual void Action(TDistributeData item, CancellationToken cancellationToken)
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
