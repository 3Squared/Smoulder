using System;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        void Start(CancellationToken cancellationToken);
        void Startup();
        void Action(CancellationToken cancellationToken);
        void Finalise();
        void OnError(Exception e);
    }
}
