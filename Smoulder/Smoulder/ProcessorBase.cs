using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class ProcessorBase<TProcessData, TDistributeData> : IProcessor<TProcessData, TDistributeData> where TProcessData : new()
    {
        private BlockingCollection<TProcessData> _processorQueue;
        private ConcurrentQueue<TProcessData> _underlyingQueue;
        private BlockingCollection<TDistributeData> _distributorQueue;
        protected int Timeout = 1000;

        public void RegisterProcessorQueue(BlockingCollection<TProcessData> processorQueue, ConcurrentQueue<TProcessData> underlyingQueue)
        {
            _processorQueue = processorQueue;
            _underlyingQueue = underlyingQueue;
        }

        public void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue)
        {
            _distributorQueue = distributorQueue;
        }

        public void Enqueue(TDistributeData itemToEnqueue)
        {
            _distributorQueue.Add(itemToEnqueue);
        }

        public int GetProcessorQueueCount()
        {
            return _processorQueue.Count;
        }

        public int GetDistributorQueueCount()
        {
            return _distributorQueue.Count;
        }

        public bool Dequeue(out TProcessData item)
        {
            return _processorQueue.TryTake(out item, Timeout);
        }

        public bool Peek(out TProcessData item)
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
                    if (_processorQueue.TryTake(out var item, Timeout, cancellationToken))
                    {
                        Action(item, cancellationToken);
                    }
                    else
                    {
                        OnEmptyQueue(cancellationToken);
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

        public virtual void Action(TProcessData processData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual void Finalise()
        {
        }

        public virtual void OnError(Exception e)
        {
        }

        public virtual void OnEmptyQueue(CancellationToken cancellationToken)
        {
        }
    }
}
