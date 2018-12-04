using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.ExampleApplication.ConcreteClasses
{
    public class Loader : LoaderBase
    {
        private int _count;
        public override void Action(CancellationToken cancellationToken)
        {
            //Console.WriteLine("Loading");
            var data = new ProcessDataObject {DataValue = _count};
            _count++;
            ProcessorQueue.Enqueue(data);
            Random rng = new Random();
            Task.Delay(rng.Next(1, 500));
        }

        public override async Task Finalise()
        {
            await Task.Delay(500);
        }
    }
}
