using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
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

        public override async Task Start(CancellationToken cancellationToken)
        {
            await Startup();
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
                    await CatchError(e);
                }
            }
        }
    }
}
