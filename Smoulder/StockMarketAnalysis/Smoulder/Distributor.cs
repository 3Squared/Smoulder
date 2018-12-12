using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;

namespace StockMarketAnalysis.Smoulder
{
    public class Distributor : DistributorBase
    {
        public override async void Action(CancellationToken cancellationToken)
        {
            if (DistributorQueue.TryDequeue(out var incomingData))
            {
                var decision = (TradeDecision) incomingData;
                Console.WriteLine($"{decision.Ticker}: {decision.TradeAction}");
            }
            else
            {
                Thread.Sleep(500);
            }
        }

        public override async Task Finalise()
        {
        }
    }
}
