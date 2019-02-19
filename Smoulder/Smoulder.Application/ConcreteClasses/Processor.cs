using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder.ExampleApplication.ConcreteClasses
{
    public class Processor : ProcessorBase
    {
        public override Task Action(CancellationToken cancellationToken)
        {
            if (ProcessorQueue.TryDequeue(out IProcessDataObject queueItem))
            {
                //Console.WriteLine("Processing");
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
                Console.WriteLine("Processor Skipped, Processor Queue is empty: " + ProcessorQueue.IsEmpty);
                Task.Delay(50);
            }
            return null;
        }

        public override async Task Finalise()
        {
            Console.WriteLine("Starting Processor finalisation." + ProcessorQueue.Count + " items left to process");

            while (ProcessorQueue.Count != 0)
            {
                if (ProcessorQueue.TryDequeue(out IProcessDataObject queueItem))
                {
                    //Console.WriteLine("Processing");
                    var data = (ProcessDataObject) queueItem;
                    var result = new DistributeDataObject
                    {
                        DataValue1 = data.DataValue,
                        DataValue2 = data.DataValue / 2
                    };

                    DistributorQueue.Enqueue(result);

                    Random rng = new Random();
                    Task.Delay(rng.Next(1, 1000));
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Finished Processor finalisation." + ProcessorQueue.Count + " items left to process");
        }
    }
}
