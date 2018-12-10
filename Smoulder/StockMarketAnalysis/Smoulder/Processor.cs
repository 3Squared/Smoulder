using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using StockMarketAnalysis.Enums;

namespace StockMarketAnalysis.Smoulder
{
    public class Processor : ProcessorBase
    {
        private decimal _previousStochK;
        private decimal _previousStochD;

        public override async void Action(CancellationToken cancellationToken)
        {
            if (ProcessorQueue.TryDequeue(out var incomingData))
            {
                //Find the thing I want to find at a data point
                var stockData = (StockData)incomingData;

                var slowK = decimal.Parse(stockData.Stoch.TechnicalIndicator.First().SlowK);
                var slowD = decimal.Parse(stockData.Stoch.TechnicalIndicator.First().SlowD);
                var stochTradeAction = AnalyseStoch(_previousStochK, _previousStochD, slowK, slowD);

                Console.WriteLine($"K {slowK} : D {slowD} : {stochTradeAction}");

                //Say buy/sell for that stock

                if (stochTradeAction != TradeAction.TradeActionEnum.none)
                {
                    var tradeDecision = new TradeDecision
                    {
                        TradeAction = stochTradeAction,
                        Ticker = stockData.Ticker
                    };
                    DistributorQueue.Enqueue(tradeDecision);
                }

                WriteToFile(slowK, slowD, stochTradeAction, stockData.Ticker);

                _previousStochD = slowD;
                _previousStochK = slowK;
            }
            else
            {
                Thread.Sleep(500);
            }
        }

        private void WriteToFile(decimal slowK, decimal slowD, TradeAction.TradeActionEnum stochTradeAction, string ticker)
        {
            try
            {
                using (var writer = new StreamWriter(@"E:\Projects\StockMarketData\MSFT-StochData.csv", append: true))
                {
                    writer.WriteLine($"{DateTime.Now},{ticker},{slowD},{slowK},{slowD > 80},{slowD < 20},{stochTradeAction}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override async Task Finalise()
        {
        }

        private TradeAction.TradeActionEnum AnalyseStoch(decimal prevK, decimal prevD, decimal currentK, decimal currentD)
        {
            if (currentD > 80 && prevK > prevD && currentD > currentK)
            {
                return Enums.TradeAction.TradeActionEnum.buy;
            }
            if (currentD < 20 && prevK < prevD && currentD < currentK)
            {
                return Enums.TradeAction.TradeActionEnum.sell;
            }
            return Enums.TradeAction.TradeActionEnum.none;
        }
    }
}
