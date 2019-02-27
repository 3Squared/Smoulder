using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        void Start(CancellationToken cancellationToken);
        void Action(CancellationToken cancellationToken);
        void Inaction(CancellationToken cancellationToken);
        void Finalise();
        void CatchError(Exception e);
    }
}
