using System;
using System.Collections.Generic;
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

            var smoulderParameters = new object[] {57000,
                "OU9SMS12HKE8MPLV",
                new List<string>
                {
                    "AAPL",
                    "ABBV",
                    "ABT",
                    "ACN",
                    "AGN",
                    "AIG",
                    "ALL",
                    "AMGN",
                    "AMZN",
                    "AXP",
                    "BA",
                    "BAC",
                    "BIIB",
                    "BK",
                    "BKNG",
                    "BLK",
                    "BMY",
                    "C",
                    "CAT",
                    "CELG",
                    "CHTR",
                    "CL",
                    "CMCSA",
                    "COF",
                    "COP",
                    "COST",
                    "CSCO",
                    "CVS"
                }
            };
            smoulder.Start(smoulderParameters).Wait();

            while (!smoulder.loaderCancellationTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(500);
            }
        }
    }
}
