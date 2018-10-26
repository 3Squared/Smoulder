using System.Collections.Concurrent;
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
    }
}
