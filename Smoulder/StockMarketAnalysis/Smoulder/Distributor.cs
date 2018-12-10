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
                //Distribute the buy/sell order to user
            }
            else
            {
                await Task.Delay(500);
            }
        }

        public override async Task Finalise()
        {
        }
    }
}
