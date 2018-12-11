using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avapi;
using Avapi.AvapiSTOCH;
using Smoulder;

namespace StockMarketAnalysis.Smoulder
{
    public class Loader : LoaderBase
    {
        private List<string> sp100;
        private List<TickerDetails> _tickers;
        private readonly string _apiKey = "OU9SMS12HKE8MPLV";

        private AvapiConnection _avapiConnection;

        public override async void Action(CancellationToken cancellationToken)
        {
            var ticker = _tickers.First();

            ticker.PreviousD = ticker.CurrentD;
            ticker.PreviousK = ticker.CurrentK;

            var stochData = GetStochForTicker(ticker.Ticker);

            if (stochData != null)
            {

                if (stochData.Error)
                {
                    Console.WriteLine($"Error getting {ticker.Ticker}: {stochData.ErrorMessage}");
                    Thread.Sleep(12000);
                    return;
                }


                ticker.CurrentD = double.Parse(stochData.TechnicalIndicator.First().SlowD);
                ticker.CurrentK = double.Parse(stochData.TechnicalIndicator.First().SlowK);

                if (ticker.PreviousD != null && ticker.PreviousK != null)
                {
                    ticker.DeltaD = (double) (ticker.CurrentD - ticker.PreviousD);
                    ticker.DeltaK = (double) (ticker.CurrentK - ticker.PreviousK);
                    ticker.OrderValue = Math.Sqrt(ticker.DeltaD * ticker.DeltaD + ticker.DeltaK * ticker.DeltaK);
                }

                Thread.Sleep(12000);
            }
            else
            {
                Console.WriteLine($"Error getting {ticker.Ticker}");
                ticker.OrderValue = 0;
            }

            if (Math.Abs(ticker.OrderValue) < 0.01)
            {
                _tickers.Remove(ticker);
                _tickers.Add(ticker);
            }
            else
            {
                _tickers.Remove(ticker);

                var halfLength = _tickers.Count / 2.0;
                var percentageThrough = ticker.OrderValue/100;
                var insertPosition = (int) Math.Ceiling(halfLength * (1 - percentageThrough));

                _tickers.Insert(insertPosition, ticker);
            }

            foreach (var sorted in _tickers)
            {
                Console.Write($"({sorted.Ticker}:{Math.Round(sorted.OrderValue,2)}) ");
            }
            Console.WriteLine();
        }

        public override async Task Startup()
        {
            sp100 = new List<string>
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
                "CVS",
                "CVX",
                "DHR",
                "DIS",
                "DUK",
                "DWDP",
                "EMR",
                "EXC",
                "F",
                "FB",
                "FDX",
                "FOX",
                "FOXA",
                "GD",
                "GE",
                "GILD",
                "GM",
                "GOOG",
                "GOOGL",
                "GS",
                "HAL",
                "HD",
                "HON",
                "IBM",
                "INTC",
                "JNJ",
                "JPM",
                "KHC",
                "KMI",
                "KO",
                "LLY",
                "LMT",
                "LOW",
                "MA",
                "MCD",
                "MDLZ",
                "MDT",
                "MET",
                "MMM",
                "MO",
                "MRK",
                "MS",
                "MSFT",
                "NEE",
                "NFLX",
                "NKE",
                "NVDA",
                "ORCL",
                "OXY",
                "PEP",
                "PFE",
                "PG",
                "PM",
                "PYPL",
                "QCOM",
                "RTN",
                "SBUX",
                "SLB",
                "SO",
                "SPG",
                "T",
                "TGT",
                "TXN",
                "UNH",
                "UNP",
                "UPS",
                "USB",
                "UTX",
                "V",
                "VZ",
                "WBA",
                "WFC",
                "WMT",
                "XOM"

            };

            _avapiConnection = AvapiConnection.Instance;
            _avapiConnection.Connect(_apiKey);
            _tickers = new List<TickerDetails>();
            foreach (var spcomany in sp100)
            {
                _tickers.Add(new TickerDetails{Ticker = spcomany});
            }
        }

        public IAvapiResponse_STOCH_Content GetStochForTicker(string ticker)
        {

            try
            {
                var stochResponse = _avapiConnection.GetQueryObject_STOCH().Query(ticker, Const_STOCH.STOCH_interval.n_1min, 10, 10, 10,
                    Const_STOCH.STOCH_slowkmatype.n_0, Const_STOCH.STOCH_slowdmatype.n_0);
                //Shove it onto queue
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

    }
}
