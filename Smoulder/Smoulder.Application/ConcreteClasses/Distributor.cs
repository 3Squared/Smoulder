using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder.Application.ConcreteClasses
{
    public class Distributor : DistributorBase
    {
        public override void Action(CancellationToken cancellationToken)
        {
            if (DistributorQueue.TryDequeue(out IDistributeDataObject queueItem))
            {
                var data = (DistributeDataObject) queueItem;
                //Console.WriteLine(data.DataValue1 + ":" + data.DataValue2);
                Random rng = new Random();
                Task.Delay(rng.Next(1, 250));
            }
            else
            {
                Console.WriteLine("Distributor Skipped, Distributor Queue is empty: " + DistributorQueue.IsEmpty);
                Task.Delay(50);
            }
        }

        public override async Task Finalise()
        {
            Random rng = new Random();
            Task.Delay(rng.Next(500, 1000));
        }
    }
}
