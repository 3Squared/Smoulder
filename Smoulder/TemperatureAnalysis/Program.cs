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
            smoulder.Start();

            Thread.Sleep(15000);

            smoulder.Stop();

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
