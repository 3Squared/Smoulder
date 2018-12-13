using System.Threading;
using Smoulder;
using TrainDataListener.Smoulder;

namespace TrainDataListener
{
    public class Program
    {
        static void Main(string[] args)
        {
            var smoulderFactory = new SmoulderFactory();
            var smoulder = smoulderFactory.Build(new Loader(), new Processor(), new Distributor());

            var connectionParameters = new ConnectionParameters
            {

            };

            smoulder.Start(connectionParameters).Wait();

            while (!smoulder.LoaderCancellationTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(500);
            }
        }
    }
}
