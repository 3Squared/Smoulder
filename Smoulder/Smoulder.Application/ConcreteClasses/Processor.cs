using System;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder.Application.ConcreteClasses
{
    public class Processor : ProcessorBase
    {
        public override void Action()
        {
            if (ProcessorQueue.TryDequeue(out IProcessDataObject queueItem))
            {
                Console.WriteLine("Processing");
                var data = (ProcessDataObject) queueItem;
                var result = new DistributeDataObject
                {
                    DataValue1 = data.DataValue,
                    DataValue2 = data.DataValue / 2
                };

                DistributorQueue.Enqueue(result);

                Random rng = new Random();
                Task.Delay(rng.Next(1,1000));
            }
            else
            {
                Console.WriteLine("Processor Skipped");
                Task.Delay(50);
            }
        }
    }
}
