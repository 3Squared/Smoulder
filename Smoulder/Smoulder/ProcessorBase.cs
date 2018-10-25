using System.Collections.Concurrent;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class ProcessorBase : IProcessor
    {
        private ConcurrentQueue<IProcessDataObject> _processorQueue;
        private ConcurrentQueue<IDistributeDataObject> _distributorQueue;

        public void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue)
        {
            _processorQueue = processorQueue;
        }

        public void RegisterDistributorQueue(ConcurrentQueue<IDistributeDataObject> distributorQueue)
        {
            _distributorQueue = distributorQueue;
        }
    }
}
