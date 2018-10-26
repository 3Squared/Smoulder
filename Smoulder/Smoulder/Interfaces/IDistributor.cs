using System.Collections.Concurrent;

namespace Smoulder.Interfaces
{
    public interface IDistributor
    {
        void RegisterDistributorQueue(ConcurrentQueue<IDistributeDataObject> distributorQueue);
    }
}
