using System;
using System.Threading;
using System.Threading.Tasks;
using Avapi;
using Avapi.AvapiEMA;
using Avapi.AvapiSTOCH;
using Smoulder;

namespace StockMarketAnalysis.Smoulder
{
    public class Loader : LoaderBase
    {
        private AvapiConnection _avapiConnection;
        public override async void Action(CancellationToken cancellationToken)
        {
            //Get a stock price from a list of stocks
            var ticker = "MSFT";

            //Int_EMA ema = _avapiConnection.GetQueryObject_EMA();
            //IAvapiResponse_EMA emaResponse =
            //    ema.Query(ticker, Const_EMA.EMA_interval.n_1min, 60, Const_EMA.EMA_series_type.open);

            Int_STOCH stoch = _avapiConnection.GetQueryObject_STOCH();
            IAvapiResponse_STOCH stochResponse;

            try
            {
                stochResponse = stoch.Query("MSFT", Const_STOCH.STOCH_interval.n_1min, 10, 10, 10,
                        Const_STOCH.STOCH_slowkmatype.n_0, Const_STOCH.STOCH_slowdmatype.n_0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            //Shove it onto queue
            var queueData = new StockData
            {
                Ema = null,
                Stoch = stochResponse.Data,
                Ticker = ticker
            };

            ProcessorQueue.Enqueue(queueData);

            //Wait 1/12 minute to not hit rate limit
            Thread.Sleep(12000);
        }

        public override async Task Startup()
        {
            _avapiConnection = AvapiConnection.Instance;
            _avapiConnection.Connect("OU9SMS12HKE8MPLV"); //TODO Make APIKey configurable
        }

        public override async Task Finalise()
        {
        }
    }
}
