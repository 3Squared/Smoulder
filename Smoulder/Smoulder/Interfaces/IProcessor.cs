using System.Collections.Concurrent;

namespace Smoulder.Interfaces
{
    public interface IProcessor : IWorkerUnit
    {
        void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue);
        void RegisterDistributorQueue(ConcurrentQueue<IDistributeDataObject> distributorQueue);
    }
}
