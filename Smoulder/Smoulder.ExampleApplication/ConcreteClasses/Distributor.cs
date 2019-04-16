using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.ExampleApplication.ConcreteClasses
{
    public class Distributor : DistributorBase<DistributeDataObject>
    {
        public override void Action(DistributeDataObject data, CancellationToken cancellationToken)
        {
                //Console.WriteLine(data.DataValue1 + ":" + data.DataValue2);
                Random rng = new Random();
                Task.Delay(rng.Next(1, 250));
        }

        public override void Finalise()
        {
            while (Dequeue(out var data))
            {
                    //Console.WriteLine(data.DataValue1 + ":" + data.DataValue2);
                    Random rng = new Random();
            }

            Console.WriteLine("Finished Distributor finalisation." + GetDistributorQueueCount() + " items left to process");
        }
    }
}
