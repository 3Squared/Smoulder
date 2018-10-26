using System.Collections.Concurrent;
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
    }
}
