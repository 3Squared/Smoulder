using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class WorkerUnitBase : IWorkerUnit
    {

        public virtual async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Run(() => Action(cancellationToken));
            }
        }

        public abstract void Action(CancellationToken cancellationToken);

        public abstract Task Finalise();
    }
}
