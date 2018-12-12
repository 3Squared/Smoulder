using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using StockMarketAnalysis.Enums;

namespace StockMarketAnalysis.Smoulder
{
    public class Distributor : DistributorBase
    {
        public override async void Action(CancellationToken cancellationToken)
        {
            if (DistributorQueue.TryDequeue(out var incomingData))
            {
                var decision = (TradeDecision) incomingData;
                Console.WriteLine($"Distributor - {decision.Ticker}: {decision.TradeAction}");
                WriteToFile(decision.TradeAction, decision.Ticker);
            }
            else
            {
                Thread.Sleep(500);
            }
        }

        public override async Task Finalise()
        {
        }

        private void WriteToFile(TradeAction.TradeActionEnum stochTradeAction, string ticker)
        {
            try
            {
                using (var writer = new StreamWriter(@"E:\Projects\StockMarketData\MSFT-TradeSignals.csv", append: true))
                {
                    writer.WriteLine(
                        $"{DateTime.Now},{ticker},{stochTradeAction}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
