using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avapi;
using Avapi.AvapiSTOCH;
using Smoulder;
using Smoulder.Interfaces;

namespace StockMarketAnalysis.Smoulder
{
    public class Loader : LoaderBase
    {
        private List<string> _sp100;
        private List<TickerDetails> _tickers;
        private int _rateLimit;

        private AvapiConnection _avapiConnection;

        public override Task Action(CancellationToken cancellationToken)
        {
            var ticker = _tickers.First();

            ticker.PreviousD = ticker.CurrentD;
            ticker.PreviousK = ticker.CurrentK;

            var stochData = GetStochForTicker(ticker.Ticker);

            if (stochData != null)
            {
                if (stochData.Error)
                {
                    WriteToConsole($"Error getting {ticker.Ticker}: {stochData.ErrorMessage}");
                    Thread.Sleep(_rateLimit);
                    return null;
                }

                ticker = UpdateTicker(ticker, stochData);
            }
            else
            {
                WriteToConsole($"Error getting {ticker.Ticker}");
                ticker.OrderValue = 0;
            }

            RepositionTicker(ticker);
            //ReportTickerOrder();
            return null;
        }

        private void ReportTickerOrder()
        {
            Console.Write("Loader - ");
            foreach (var sorted in _tickers)
            {
                Console.Write($"({sorted.Ticker}:{Math.Round(sorted.OrderValue, 2)}) ");
            }
            Console.WriteLine();
        }

        private TickerDetails UpdateTicker(TickerDetails ticker, IAvapiResponse_STOCH_Content stochData)
        {
            ticker.CurrentD = double.Parse(stochData.TechnicalIndicator.First().SlowD);
            ticker.CurrentK = double.Parse(stochData.TechnicalIndicator.First().SlowK);

            if (ticker.PreviousD != null && ticker.PreviousK != null)
            {
                ticker.DeltaD = (double) (ticker.CurrentD - ticker.PreviousD);
                ticker.DeltaK = (double) (ticker.CurrentK - ticker.PreviousK);
                ticker.OrderValue = Math.Sqrt(ticker.DeltaD * ticker.DeltaD + ticker.DeltaK * ticker.DeltaK);
            }

            Thread.Sleep(_rateLimit);
            return ticker;
        }

        private void RepositionTicker(TickerDetails ticker)
        {
            if (Math.Abs(ticker.OrderValue) < 0.01)
            {
                _tickers.Remove(ticker);
                _tickers.Add(ticker);
                WriteToConsole($"{ticker.Ticker}: No Change sending to {_tickers.IndexOf(ticker)}");
            }
            else
            {
                _tickers.Remove(ticker);

                var halfLength = _tickers.Count / 2.0;
                var percentageThrough = ticker.OrderValue / 100;
                var insertPosition = (int) Math.Ceiling(halfLength * (1 - percentageThrough));

                _tickers.Insert(insertPosition, ticker);

                WriteToConsole($"{ticker.Ticker}: Change {ticker.OrderValue}, sending to {_tickers.IndexOf(ticker)}");
            }
        }

        public IAvapiResponse_STOCH_Content GetStochForTicker(string ticker)
        {
            try
            {
                var stochResponse = _avapiConnection.GetQueryObject_STOCH().Query(ticker,
                    Const_STOCH.STOCH_interval.n_1min, 10, 10, 10,
                    Const_STOCH.STOCH_slowkmatype.n_0, Const_STOCH.STOCH_slowdmatype.n_0);

                var queueData = new StockData
                {
                    Ema = null,
                    Stoch = stochResponse.Data,
                    Ticker = ticker
                };

                ProcessorQueue.Enqueue(queueData);

                return stochResponse.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        private void WriteToConsole(string output)
        {
            //Console.WriteLine("Loader - " + output);
        }

        public override async Task Startup(IStartupParameters startupParameters)
        {
            var parameters = (StartupParameters) startupParameters;
            _rateLimit = parameters.RateLimit;
            _avapiConnection = AvapiConnection.Instance;
            _avapiConnection.Connect(parameters.ApiKey);
            _sp100 = parameters.Companies;
            _tickers = new List<TickerDetails>();
            foreach (var spcomany in _sp100)
            {
                _tickers.Add(new TickerDetails {Ticker = spcomany});
            }
        }
    }
}
