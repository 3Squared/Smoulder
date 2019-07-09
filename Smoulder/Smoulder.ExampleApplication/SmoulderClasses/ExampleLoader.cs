using System;
using System.Threading;

namespace Smoulder.ExampleApplication.SmoulderClasses
{
    public class ExampleLoader : LoaderBase<ProcessDataObject>
    {
        private int _count;
        private Random _rng;
        public override ProcessDataObject Action(CancellationToken cancellationToken)
        {
            //Get some data from upstream process
            var data = new ProcessDataObject { DataValue = _count};
            _count++;

            //Simulate some processing time
            Thread.Sleep(_rng.Next(1, 500));

            //Send it to Processor
            return data;
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
            throw new Exception("Throw loader exception with inner exception attached", e);
        }
    }
}
