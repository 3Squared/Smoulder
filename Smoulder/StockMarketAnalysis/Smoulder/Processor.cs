using System.Threading;
using System.Threading.Tasks;
using Smoulder;

namespace StockMarketAnalysis.Smoulder
{
    public class Processor : ProcessorBase
    {
        public override async void Action(CancellationToken cancellationToken)
        {
            if (ProcessorQueue.TryDequeue(out var incomingData))
            {
                //Find the thing I want to find at a data point
                //Say buy/sell for that stock
            }
            else
            {
                await Task.Delay(500);
            }
        }
    }
}
