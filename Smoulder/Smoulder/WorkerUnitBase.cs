using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class WorkerUnitBase : IWorkerUnit
    {
        public ConcurrentQueue<IDataObject> UpstreamQueue;
        public ConcurrentQueue<IDataObject> DownstreamQueue;

        public void RegisterUpstreamQueue(ConcurrentQueue<IDataObject> upstreamQueue)
        {
            UpstreamQueue = upstreamQueue;
        }

        public void RegisterDownstreamQueue(ConcurrentQueue<IDataObject> downstreamQueue)
        {
            DownstreamQueue = downstreamQueue;
        }

        public virtual async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Run(() => Action(cancellationToken));
            }
        }

        public virtual async void Action(CancellationToken cancellationToken)
        {
            await Task.Delay(100);
        }

        public virtual async Task Finalise()
        {
        }
    }
}
