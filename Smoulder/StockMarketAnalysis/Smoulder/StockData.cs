using Avapi.AvapiEMA;
using Avapi.AvapiSTOCH;
using Smoulder.Interfaces;

namespace StockMarketAnalysis.Smoulder
{
    public class StockData : IProcessDataObject
    {
        public string Ticker;
        public IAvapiResponse_EMA_Content Ema;
        public IAvapiResponse_STOCH_Content Stoch;
    }
}
