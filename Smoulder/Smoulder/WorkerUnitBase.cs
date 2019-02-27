using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class WorkerUnitBase : IWorkerUnit
    {
        public abstract void Start(CancellationToken cancellationToken);

        public abstract void Action(CancellationToken cancellationToken);

        public virtual void Inaction(CancellationToken cancellationToken)
        {
            Thread.Sleep(1000);
        }

        public virtual void Finalise()
        {
        }

        public virtual void Startup()
        {
        }

        public virtual void CatchError(Exception e)
        {
            throw e;
        }
    }
}
