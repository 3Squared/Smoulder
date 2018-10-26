using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Smoulder.Concrete;

namespace Smoulder.Application.ConcreteClasses
{
    public class Loader : LoaderBase
    {
        private int _count;
        public override void Action()
        {
            //Console.WriteLine("Loading");
            var data = new ProcessDataObject {DataValue = _count};
            _count++;
            ProcessorQueue.Enqueue(data);
            Random rng = new Random();
            Task.Delay(rng.Next(1, 500));
        }
    }
}
