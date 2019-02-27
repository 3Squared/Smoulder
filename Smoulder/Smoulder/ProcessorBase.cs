using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class ProcessorBase : WorkerUnitBase, IProcessor
    {
        public ConcurrentQueue<IProcessDataObject> ProcessorQueue;
        public ConcurrentQueue<IDistributeDataObject> DistributorQueue;

        public void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue)
        {
            ProcessorQueue = processorQueue;
        }

        public void RegisterDistributorQueue(ConcurrentQueue<IDistributeDataObject> distributorQueue)
        {
            DistributorQueue = distributorQueue;
        }

        public override void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (ProcessorQueue.IsEmpty)
                    {
                        Inaction(cancellationToken);

                    }
                    else
                    {
                        Action(cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    CatchError(e);
                }
            }
        }
    }
}
