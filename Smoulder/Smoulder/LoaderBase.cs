using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class LoaderBase : WorkerUnitBase, ILoader
    {
        public ConcurrentQueue<IProcessDataObject> ProcessorQueue;

        public void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue)
        {
            ProcessorQueue = processorQueue;
        }


        public override async Task Start(CancellationToken cancellationToken)
        {
            await Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {

                    await Action(cancellationToken);
                }
                catch (Exception e)
                {
                    CatchError(e);
                    throw;
                }
            }
        }

        public override Task Action(CancellationToken cancellationToken)
        {
            Thread.Sleep(1000);
            return null;
        }

        public override Task CatchError(Exception e)
        {
            return null;
        }
    }
}
