using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smoulder.ExampleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fakeRepository = new ExampleRepository();
            var smoulderFactory = new SmoulderFactory();

            //Object oriented methodology
            var firstSmoulder = smoulderFactory.Build(new ExampleLoader(), new ExampleProcessor(fakeRepository), new ExampleDistributor(), 50000);

            //Run Demo with first Smoulder
            RunDemo(firstSmoulder);


            //Functional methodology
            //Create second smoulder by passing methods to factory, thus removing the requirement to instantiate any worker units
            var secondSmoulder = smoulderFactory.Build<ProcessDataObject, DistributeDataObject>(null, null, null, 50000)
                .SetLoaderAction(token =>
                {
                    var rng = new Random();
                    var data = new ProcessDataObject {DataValue = rng.Next()};
                    Task.Delay(rng.Next(1, 50));
                    return data;
                })
                .SetProcessorAction((data, token) =>
                {
                    fakeRepository.SaveData(data);
                    var result = new DistributeDataObject
                    {
                        DataValue1 = data.DataValue,
                        DataValue2 = data.DataValue / 2
                    };

                    Random rng = new Random();
                    Task.Delay(rng.Next(1, 100));

                    return result;
                })
                .SetDistributorAction((data, token) =>
                {
                    Random rng = new Random();
                    Task.Delay(rng.Next(1, 25));
                })
                .SetLoaderOnError(e => throw new Exception("Throw loader exception with inner exception attached", e))
                .SetProcessorOnError(e => throw new Exception("Throw processor exception with inner exception attached", e))
                .SetDistributorOnError(e => throw new Exception("Throw distributor exception with inner exception attached", e));

            //Run Demo with second Smoulder
            RunDemo(secondSmoulder);


            //Object oriented methodology with some functional stuff to save having to create oversimple worker units
            //Create third smoulder by mixing the two previous methods
            var thirdSmoulder = smoulderFactory.Build(new ExampleLoader(), new ExampleProcessor(fakeRepository), null, 50000)
                .SetDistributorAction((data, token) =>
                {
                    Random rng = new Random();
                    Task.Delay(rng.Next(1, 25));
                });

            //Run Demo with third Smoulder
            RunDemo(thirdSmoulder);

            Console.WriteLine("Press enter to finish the Smoulder demonstration process");
            Console.ReadLine();
        }

        private static void GetReport(Smoulder<ProcessDataObject, DistributeDataObject> smoulder)
        {
            Console.WriteLine("Processor Items: " + smoulder.ProcessorQueueItems);
            Console.WriteLine("Distributor Items: " + smoulder.DistributorQueueItems);
        }

        private static void RunDemo(Smoulder<ProcessDataObject, DistributeDataObject> smoulder)
        {
            Console.WriteLine("--------- Beginning Smoulder demo run ---------");
            //Nothing is running yet, so both queues have 0 items on them
            GetReport(smoulder);

            //Start smoulder running at system start up
            smoulder.Start();

            //Queues now have items on them, data filtering through smoulder
            GetReport(smoulder);

            //Leave smoulder running indefinately
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                GetReport(smoulder);
            }

            //System can be paused, stop the smoulder object
            smoulder.Stop();

            Console.WriteLine("Smoulder has been paused, waiting for 1.5 seconds before restarting");
            Thread.Sleep(1500);
            Console.WriteLine("Smoulder restarting");
            //Smoulder can be restarted
            smoulder.Start();

            GetReport(smoulder);

            for (var i = 0; i < 50; i++)
            {
                Thread.Sleep(100);
                GetReport(smoulder);
            }

            smoulder.Stop();

            GetReport(smoulder);
            Console.WriteLine("--------- Ending Smoulder demo run ---------");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
