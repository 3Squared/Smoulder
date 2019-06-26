using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.ExampleApplication
{
    public class ExampleProcessor : ProcessorBase<ProcessDataObject,DistributeDataObject>
    {
        ExampleRepository _repostiory;

        public ExampleProcessor(ExampleRepository repostiory)
        {
            //Constructor is called on creation of Object, do any dependency injection here
            _repostiory = repostiory;
        }

        public override void Action(ProcessDataObject data, CancellationToken cancellationToken)
        {
            //Pretend to save the data to an archive
            _repostiory.SaveData(data);

            //Do some operation on the data to create a result
            var result = new DistributeDataObject
            {
                DataValue1 = data.DataValue,
                DataValue2 = data.DataValue / 2
            };

            //Send result to Distributor
            Enqueue(result);

            //Simulate some processing time
            Random rng = new Random();
            Task.Delay(rng.Next(1, 1000));
        }

        public override void Startup()
        {
            //Startup called if smoulder is turned off and back on again
            _repostiory.ResetConnection();
            Console.WriteLine("Processor Initialised");
        }

        public override void Finalise()
        {
            Console.WriteLine("Starting Processor finalisation." + GetProcessorQueueCount() + " items left to process");

            while (Dequeue(out var data))
            {
                Action(data, new CancellationToken());
            }

            _repostiory.Dispose();

            Console.WriteLine("Finished Processor finalisation." + GetProcessorQueueCount() + " items left to process");
        }

        public override void OnError(Exception e)
        {
            throw new Exception("Throw processor exception with inner exception attached",e);
        }
    }
}
