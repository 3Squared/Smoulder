using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class LoaderBase<T> : ILoader<T> where T : new()
    {
        private ConcurrentQueue<T> _processorQueue;

        public void RegisterProcessorQueue(ConcurrentQueue<T> processorQueue)
        {
            _processorQueue = processorQueue;
        }

        public int GetProcessorQueueCount()
        {
            return _processorQueue.Count;
        }

        public void Enqueue(T itemToEnqueue)
        {
            _processorQueue.Enqueue(itemToEnqueue);
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
