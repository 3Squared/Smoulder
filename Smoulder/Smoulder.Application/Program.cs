using System;
using System.Threading.Tasks;
using Smoulder.Application.ConcreteClasses;

namespace Smoulder.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var smoulderFactory = new SmoulderFactory();
            var smoulder = smoulderFactory.Build(new Loader(), new Processor(), new Distributor());

            GetReport(smoulder);

            smoulder.Start();

            GetReport(smoulder);

            for (int i = 0; i < 50; i++)
            {
                System.Threading.Thread.Sleep(100);
                GetReport(smoulder);
            }

            Task.Factory.StartNew(() => smoulder.Stop());

            GetReport(smoulder);

            System.Threading.Thread.Sleep(10000);
            GetReport(smoulder);
        }

        public static void GetReport(Smoulder smoulder)
        {
            Console.WriteLine("Processor Items: " + smoulder.ProcessorQueueItems);
            Console.WriteLine("Distributor Items: " + smoulder.DistributorQueueItems);
        }
    }
}
