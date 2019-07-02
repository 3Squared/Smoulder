using System;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        void Start(CancellationToken cancellationToken);

        void Startup();
        void SetStartup(Action startup);

        void Finalise();
        void SetFinalise(Action finalise);

        void OnError(Exception e);
        void SetOnError(Action<Exception> onError);
    }
}
