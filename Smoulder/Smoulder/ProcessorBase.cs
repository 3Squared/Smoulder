using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class ProcessorBase<TProcessData, TDistributeData> : IProcessor<TProcessData, TDistributeData>
    {
        private ConcurrentQueue<TProcessData> _processorQueue;
        private ConcurrentQueue<TDistributeData> _distributorQueue;

        public void RegisterProcessorQueue(ConcurrentQueue<TProcessData> processorQueue)
        {
            _processorQueue = processorQueue;
        }

        public void RegisterDistributorQueue(ConcurrentQueue<TDistributeData> distributorQueue)
        {
            _distributorQueue = distributorQueue;
        }

        public TProcessData Dequeue()
        {
            _processorQueue.TryDequeue(out var item);
            return item;
        }

        public void Enqueue(TDistributeData itemToEnqueue)
        {
            _distributorQueue.Enqueue(itemToEnqueue);
        }

        public int GetProcessorQueueCount()
        {
            return _processorQueue.Count;
        }

        public int GetDistributorQueueCount()
        {
            return _distributorQueue.Count;
        }

        public void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_processorQueue.IsEmpty)
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
