using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder.ExampleApplication.ConcreteClasses
{
    public class Distributor : DistributorBase<DistributeDataObject>
    {
        public override void Action(CancellationToken cancellationToken)
        {
                var data = Dequeue();
                //Console.WriteLine(data.DataValue1 + ":" + data.DataValue2);
                Random rng = new Random();
                Task.Delay(rng.Next(1, 250));
        }

        public override void Finalise()
        {
            while (GetDistributorQueueCount() != 0)
            {
                    var data = Dequeue();
                    //Console.WriteLine(data.DataValue1 + ":" + data.DataValue2);
                    Random rng = new Random();
            }

            Console.WriteLine("Finished Distributor finalisation." + GetDistributorQueueCount() + " items left to process");
        }
    }
}
