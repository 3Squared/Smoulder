using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.ExampleApplication
{
    public class ExampleLoader : LoaderBase<ProcessDataObject>
    {
        private int _count;
        private Random _rng;
        public override void Action(CancellationToken cancellationToken)
        {
            //Get some data from upstream process
            var data = new ProcessDataObject { DataValue = _count};
            _count++;

            //Send it to Processor
            Enqueue(data);

            //Simulate some processing time
            Task.Delay(_rng.Next(1, 500));
        }

        public override void Startup()
        {
            //Initialise some variables
            _rng = new Random();
            _count = 0;
            Console.WriteLine("Loader initialised");
        }

        public override void Finalise()
        {
            Console.WriteLine("Loader Finalised:");
        }

        public override void OnError(Exception e)
        {
            Console.WriteLine("Error caught in Loader:" + e);
        }
    }
}
