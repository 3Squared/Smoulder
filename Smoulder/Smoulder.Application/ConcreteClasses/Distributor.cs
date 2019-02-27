using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder.ExampleApplication.ConcreteClasses
{
    public class Distributor : DistributorBase
    {
        public override Task Action(CancellationToken cancellationToken)
        {
            if (DistributorQueue.TryDequeue(out IDistributeDataObject queueItem))
            {
                var data = (DistributeDataObject) queueItem;
                //Console.WriteLine(data.DataValue1 + ":" + data.DataValue2);
                Random rng = new Random();
                Task.Delay(rng.Next(1, 250));
            }
            return null;
        }

        public override async Task Finalise()
        {
            Console.WriteLine("Starting Distributor finalisation." + DistributorQueue.Count + " items left to process");

            while (DistributorQueue.Count != 0)
            {
                if (DistributorQueue.TryDequeue(out IDistributeDataObject queueItem))
                {
                    var data = (DistributeDataObject)queueItem;
                    //Console.WriteLine(data.DataValue1 + ":" + data.DataValue2);
                    Random rng = new Random();
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Finished Distributor finalisation." + DistributorQueue.Count + " items left to process");
        }
    }
}
