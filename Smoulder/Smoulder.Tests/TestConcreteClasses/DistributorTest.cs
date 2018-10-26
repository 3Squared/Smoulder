using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Smoulder.Concrete;
using Smoulder.Interfaces;

namespace Smoulder.Tests.TestConcreteClasses
{
    public class DistributorTest : DistributorBase
    {
        public override void Action()
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
