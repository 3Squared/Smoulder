using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class ProcessorBase<TProcessData, TDistributeData> : IProcessor<TProcessData, TDistributeData> where TProcessData : new()
    {
        private ConcurrentQueue<TProcessData> _processorQueue;
        private BlockingCollection<TProcessData> _blockingProcessorQueue;
        private BlockingCollection<TDistributeData> _blockingDistributorQueue;
        protected int Timeout = 1000;

        public void RegisterProcessorQueue(ConcurrentQueue<TProcessData> processorQueue)
        {
            _processorQueue = processorQueue;
            _blockingProcessorQueue = new BlockingCollection<TProcessData>(processorQueue);
        }

        public void RegisterDistributorQueue(ConcurrentQueue<TDistributeData> distributorQueue)
        {
            _blockingDistributorQueue = new BlockingCollection<TDistributeData>(distributorQueue);
        }

        public void Enqueue(TDistributeData itemToEnqueue)
        {
            _blockingDistributorQueue.Add(itemToEnqueue);
        }

        public int GetProcessorQueueCount()
        {
            return _blockingProcessorQueue.Count;
        }

        public int GetDistributorQueueCount()
        {
            return _blockingDistributorQueue.Count;
        }

        public bool Peek(out TProcessData item)
        {
            return _processorQueue.TryPeek(out item);
        }

        public bool Dequeue(out TProcessData item)
        {
            return _blockingProcessorQueue.TryTake(out item, Timeout);
        }

        public void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_blockingProcessorQueue.TryTake(out var item, Timeout, cancellationToken))
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

        public virtual void OnNoQueueItem(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
