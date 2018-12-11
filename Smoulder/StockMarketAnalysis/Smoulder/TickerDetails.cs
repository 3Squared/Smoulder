namespace StockMarketAnalysis.Smoulder
{
    public class TickerDetails
    {
        public string Ticker;
        public double? PreviousD;
        public double? PreviousK;
        public double? CurrentD;
        public double? CurrentK;
        public double DeltaD;
        public double DeltaK;
        public double OrderValue = 0;
    }
}
