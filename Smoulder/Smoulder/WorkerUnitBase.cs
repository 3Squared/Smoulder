using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class WorkerUnitBase : IWorkerUnit
    {
        public abstract Task Start(CancellationToken cancellationToken);

        public abstract Task Action(CancellationToken cancellationToken);

        public virtual Task Inaction(CancellationToken cancellationToken)
        {
            Thread.Sleep(1000);
            return null;
        }

        public virtual async Task Finalise()
        {
        }

        public virtual async Task Startup()
        {
        }

        public virtual async Task CatchError(Exception e)
        {
            throw e;
        }
    }
}
