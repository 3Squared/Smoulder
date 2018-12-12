using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using Smoulder.Interfaces;
using StockMarketAnalysis.Enums;

namespace StockMarketAnalysis.Smoulder
{
    public class Processor : ProcessorBase
    {
        private Dictionary<string, TickerDetails> _tickers;
        private TickerDetails _workingTicker;

        public override async void Action(CancellationToken cancellationToken)
        {
            if (ProcessorQueue.TryDequeue(out var incomingData))
            {
                var stockData = (StockData) incomingData;

                var slowK = double.Parse(stockData.Stoch.TechnicalIndicator.First().SlowK);
                var slowD = double.Parse(stockData.Stoch.TechnicalIndicator.First().SlowD);

                if (!_tickers.ContainsKey(stockData.Ticker))
                {
                    _tickers.Add(stockData.Ticker, new TickerDetails
                    {
                        Ticker = stockData.Ticker,
                        PreviousD = slowD,
                        PreviousK = slowK
                    });

                Console.WriteLine($"Adding {stockData.Ticker}: K {slowK} , D {slowD} ");
                    return;
                }

                _workingTicker = _tickers[stockData.Ticker];


                var stochTradeAction = AnalyseStoch(
                    _workingTicker.PreviousK,
                    _workingTicker.PreviousD,
                    _workingTicker.CurrentK,
                    _workingTicker.CurrentD);

                Console.WriteLine($"{stockData.Ticker}: K {slowK} , D {slowD} : {stochTradeAction}");
                Console.WriteLine();

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

                _workingTicker.PreviousD = _workingTicker.CurrentD;
                _workingTicker.PreviousK = _workingTicker.CurrentK;

                _tickers[_workingTicker.Ticker] = _workingTicker;
            }
            else
            {
                Thread.Sleep(500);
            }
        }

        private void WriteToFile(double slowK, double slowD, TradeAction.TradeActionEnum stochTradeAction, string ticker)
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

        public override async Task Startup(IStartupParameters startupParameters)
        {
            _tickers = new Dictionary<string, TickerDetails>();
        }

        private TradeAction.TradeActionEnum AnalyseStoch(double? prevK, double? prevD, double? currentK, double? currentD)
        {
            if (currentD > 80 && prevK > prevD && currentD > currentK)
            {
                return TradeAction.TradeActionEnum.buy;
            }
            if (currentD < 20 && prevK < prevD && currentD < currentK)
            {
                return TradeAction.TradeActionEnum.sell;
            }
            return TradeAction.TradeActionEnum.none;
        }
    }
}
