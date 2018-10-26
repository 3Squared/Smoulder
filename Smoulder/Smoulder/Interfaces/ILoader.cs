using System.Collections.Concurrent;

namespace Smoulder.Interfaces
{
    public interface ILoader
    {
        void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue);
    }
}
