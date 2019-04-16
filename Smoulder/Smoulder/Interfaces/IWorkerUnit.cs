using System;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        void Start(CancellationToken cancellationToken);
        void Startup();
        void Finalise();
        void OnError(Exception e);
    }
}
