using System;

namespace Smoulder.ExampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var fakeRepository = new ExampleRepository();
            var smoulderFactory = new SmoulderFactory();

            //Smoulder created using poor mans dependency injection
            var smoulder = smoulderFactory.Build(new ExampleLoader(), new ExampleProcessor(fakeRepository), new ExampleDistributor());

            //Nothing is running yet, so both queues have 0 items on them
            GetReport(smoulder);

            //Start smoulder running at system start up
            smoulder.Start();

            //Queues now have items on them, data filtering through smoulder
            GetReport(smoulder);

            //Leave smoulder running indefinately
            for (var i = 0; i < 50; i++)
            {
                System.Threading.Thread.Sleep(100);
                GetReport(smoulder);
            }

            //System can be paused, stop the smoulder object
            smoulder.Stop();

            Console.WriteLine("Smoulder has been paused, waiting for 3 seconds before restarting");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Smoulder restarting");
            //Smoulder can be restarted
            smoulder.Start();

            GetReport(smoulder);

            for (var i = 0; i < 50; i++)
            {
                System.Threading.Thread.Sleep(100);
                GetReport(smoulder);
            }

            smoulder.Stop();

            GetReport(smoulder);
            Console.WriteLine("Press enter to finish the Smoulder demonstration process");
            Console.ReadLine();
        }

        public static void GetReport(Smoulder<ProcessDataObject, DistributeDataObject> smoulder)
        {
            Console.WriteLine("Processor Items: " + smoulder.ProcessorQueueItems);
            Console.WriteLine("Distributor Items: " + smoulder.DistributorQueueItems);
        }
    }
}
