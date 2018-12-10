using System;
using System.Threading;
using Smoulder;
using StockMarketAnalysis.Smoulder;

namespace StockMarketAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var smoulderFactory = new SmoulderFactory();
            var smoulder = smoulderFactory.Build(new Loader(), new Processor(), new Distributor());
            smoulder.Start().Wait();

            while (!smoulder.loaderCancellationTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(500);
            }
        }
    }
}
