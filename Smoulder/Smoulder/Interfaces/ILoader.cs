using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface ILoader : IWorkerUnit
    {
        void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue);
    }
}
