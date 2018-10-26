using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder.Tests.TestConcreteClasses
{
    public class DistributorTest : DistributorBase
    {
        public override void Action(CancellationToken cancellationToken)
        {
            if (DistributorQueue.TryDequeue(out IDistributeDataObject queueItem))
            {
                var data = (DistributeDataObjectTest) queueItem;
                Console.WriteLine(data);
            }
            else
            {
                Task.Delay(50);
            }
        }
    }
}
