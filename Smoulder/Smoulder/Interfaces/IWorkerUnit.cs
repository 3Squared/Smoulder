using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        Task Start(CancellationToken cancellationToken);
        Task Action(CancellationToken cancellationToken);
        Task Inaction(CancellationToken cancellationToken);
        Task Finalise();
        Task CatchError(Exception e);
    }
}
