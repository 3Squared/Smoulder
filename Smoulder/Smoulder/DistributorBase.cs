using System.Collections.Concurrent;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class DistributorBase : WorkerUnitBase, IDistributor
    {
        public ConcurrentQueue<IDistributeDataObject> DistributorQueue;

        public void RegisterDistributorQueue(ConcurrentQueue<IDistributeDataObject> distributorQueue)
        {
            DistributorQueue = distributorQueue;
        }
    }
}
