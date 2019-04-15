using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class ProcessorBase<TProcessData, TDistributeData> : WorkerUnitBase, IProcessor<TProcessData, TDistributeData>
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

        public override void Start(CancellationToken cancellationToken)
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
    }
}
