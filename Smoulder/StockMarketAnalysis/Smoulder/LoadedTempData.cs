using System;
using Smoulder.Interfaces;

namespace TemperatureAnalysis.Smoulder
{
    public class LoadedTempData : IProcessDataObject
    {
        public int Id;
        public DateTime Time;
        public decimal Temperature;
    }
}
