using System.Collections.Generic;
using System.Threading;
using Smoulder;
using StockMarketAnalysis.Smoulder;

namespace StockMarketAnalysis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var smoulderFactory = new SmoulderFactory();
            var smoulder = smoulderFactory.Build(new Loader(), new Processor(), new Distributor());

            //These parameters could come from anywhere, but I don't have a database/config file to pull them from
            var startupParameters = new StartupParameters {RateLimit = 60*1000,
                ApiKey = "OU9SMS12HKE8MPLV",
                Companies = new List<string>
                {
                    "SPY",
                    "BAC",
                    "VXX",
                    "EEM",
                    "AAPL",
                    "TSLA"
                }
            };

            smoulder.Start(startupParameters).Wait();

            while (!smoulder.LoaderCancellationTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(500);
            }
        }
    }
}
