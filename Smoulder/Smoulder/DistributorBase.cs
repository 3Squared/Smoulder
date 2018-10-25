using System.Collections.Concurrent;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class DistributorBase : IDistributor
    {
        private ConcurrentQueue<IDistributeDataObject> _distributorQueue;

        public void RegisterDistributorQueue(ConcurrentQueue<IDistributeDataObject> distributorQueue)
        {
            _distributorQueue = distributorQueue;
        }
    }
}
