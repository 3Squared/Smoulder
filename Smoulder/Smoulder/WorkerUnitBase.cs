using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class WorkerUnitBase : IWorkerUnit
    {
        public async Task Start(CancellationToken cancellationToken, IStartupParameters startupParameters)
        {
            await Startup(startupParameters);
            while (!cancellationToken.IsCancellationRequested)
            {
                Action(cancellationToken);
            }
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            await Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Factory.StartNew(() => Action(cancellationToken), cancellationToken);
            }
        }

        public abstract Task Action(CancellationToken cancellationToken);

        public virtual async Task Finalise()
        {
        }

        public virtual async Task Startup(IStartupParameters startupParameters)
        {
        }

        public virtual async Task Startup()
        {
        }
    }
}
