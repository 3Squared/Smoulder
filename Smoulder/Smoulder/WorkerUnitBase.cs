using System;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class WorkerUnitBase : IWorkerUnit
    {
        public abstract void Start(CancellationToken cancellationToken);

        public abstract void Action(CancellationToken cancellationToken);

        public virtual void OnNoQueueItem(CancellationToken cancellationToken)
        {
        }

        public virtual void Finalise()
        {
        }

        public virtual void Startup()
        {
        }

        public virtual void OnError(Exception e)
        {
            throw e;
        }
    }
}
