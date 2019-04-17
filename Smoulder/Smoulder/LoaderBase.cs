using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class LoaderBase<T> : ILoader<T> where T : new()
    {
        private BlockingCollection<T> _processorQueue;

        public void RegisterProcessorQueue(BlockingCollection<T> processorQueue)
        {
            _processorQueue = processorQueue;
        }

        public int GetProcessorQueueCount()
        {
            return _processorQueue.Count;
        }

        public void Enqueue(T itemToEnqueue)
        {
            _processorQueue.Add(itemToEnqueue);
        }

        public void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Action(cancellationToken);
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
    }
}
