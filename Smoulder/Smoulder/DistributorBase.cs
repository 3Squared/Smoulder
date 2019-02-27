using System;
using System.Collections.Concurrent;
using System.Threading;
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

        public override void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (DistributorQueue.IsEmpty)
                    {
                        Inaction(cancellationToken);

                    }
                    else
                    {
                        Action(cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    CatchError(e);
                }
            }
        }
    }
}
