using System.Collections.Generic;
using Smoulder.Interfaces;

namespace StockMarketAnalysis.Smoulder
{
    public class StartupParameters : IStartupParameters
    {
        public int RateLimit;
        public List<string> Companies;
        public string ApiKey;
    }
}
