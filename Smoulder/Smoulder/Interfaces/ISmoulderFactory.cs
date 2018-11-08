using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Smoulder.Interfaces
{
    internal interface ISmoulderFactory
    {
        Smoulder Build(List<IWorkerUnit> workerUnits);
    }
}
