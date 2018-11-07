using System;
using System.Threading;
using Smoulder;
using TemperatureAnalysis.Smoulder;

namespace TemperatureAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var smoulderFactory = new SmoulderFactory();
            var smoulder = smoulderFactory.Build(new Loader(), new Processor(), new Distributor());
            smoulder.Start().Wait();

            Thread.Sleep(50000);

            smoulder.Stop().Wait();

            Console.ReadLine();
        }


        // TODO Having Smoulder.Smoulder sucks, need a name change for one of them
        public void StartSmoulder(global::Smoulder.Smoulder smoulder)
        {
            
        }
    }
}
