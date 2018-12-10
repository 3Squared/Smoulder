using Smoulder.Interfaces;
using StockMarketAnalysis.Enums;

namespace StockMarketAnalysis.Smoulder
{
    public class TradeDecision : IDistributeDataObject
    {
        public TradeAction.TradeActionEnum TradeAction;
        public string Ticker;

    }
}
