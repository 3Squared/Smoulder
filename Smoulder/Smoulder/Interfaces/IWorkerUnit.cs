using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        Task Start(CancellationToken cancellationToken, IStartupParameters startupParameters);
        Task Start(CancellationToken cancellationToken);
        void Action(CancellationToken cancellationToken);
        Task Finalise();
    }
}
