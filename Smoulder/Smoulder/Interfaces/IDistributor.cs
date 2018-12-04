using System.Collections.Concurrent;

namespace Smoulder.Interfaces
{
    public interface IDistributor : IWorkerUnit
    {
        void RegisterDistributorQueue(ConcurrentQueue<IDistributeDataObject> distributorQueue);
    }
}
