using System.Collections.Concurrent;

namespace Smoulder.Interfaces
{
    public interface ILoader<T> : IWorkerUnit
    {
        void RegisterProcessorQueue(ConcurrentQueue<T> processorQueue);
        void Enqueue(T itemToEnqueue);
    }
}
