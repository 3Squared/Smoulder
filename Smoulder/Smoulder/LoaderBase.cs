using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class LoaderBase<T> : WorkerUnitBase, ILoader<T>
    {
        private ConcurrentQueue<T> _processorQueue;

        public void RegisterProcessorQueue(ConcurrentQueue<T> processorQueue)
        {
            _processorQueue = processorQueue;
        }

        public override void Start(CancellationToken cancellationToken)
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
    }
}
