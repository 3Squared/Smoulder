using System;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder.Application.ConcreteClasses
{
    public class Distributor : DistributorBase
    {
        private int _count = 0;

        public override void Action()
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
    }
}
