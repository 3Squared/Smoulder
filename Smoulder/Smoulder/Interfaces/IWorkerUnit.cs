using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface IWorkerUnit
    {
        Task Start(CancellationToken cancellationToken, params object[] args);
        void Action(CancellationToken cancellationToken);
        Task Finalise();
    }
}
