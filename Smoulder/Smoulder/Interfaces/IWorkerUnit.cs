using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        void RegisterUpstreamQueue(ConcurrentQueue<IDataObject> upstreamQueue);
        void RegisterDownstreamQueue(ConcurrentQueue<IDataObject> downstreamQueue);

        Task Start(CancellationToken cancellationToken);
        void Action(CancellationToken cancellationToken);
        Task Finalise();
    }
}
