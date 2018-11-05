using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class WorkerUnitBase : IWorkerUnit
    {

        public virtual async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Run(() => Action(cancellationToken));
            }
        }

        public virtual async Task Action(CancellationToken cancellationToken)
        {
            await Task.Delay(100);
        }

        public virtual async Task Finalise()
        {
        }
    }
}
