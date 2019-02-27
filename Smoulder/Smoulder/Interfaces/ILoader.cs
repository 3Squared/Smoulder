using System.Collections.Concurrent;

namespace Smoulder.Interfaces
{
    public interface ILoader : IWorkerUnit
    {
        void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue);
    }
}
