using System;
using System.Threading.Tasks;

namespace Smoulder.Application.ConcreteClasses
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

            smoulder.Stop();
            GetReport(smoulder);

            Task.Delay(5000);
        }

        public static void GetReport(Smoulder smoulder)
        {
            Console.WriteLine("Distributor Items: " + smoulder.DistributorQueueItems);
            Console.WriteLine("Processor Items: " + smoulder.ProcessorQueueItems);
            Console.WriteLine("IsRunning: " + smoulder.IsRunning);
        }
    }
}
