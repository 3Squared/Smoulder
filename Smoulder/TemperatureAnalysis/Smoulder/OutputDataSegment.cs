using System.Collections.Generic;
using Smoulder.Interfaces;
using TemperatureAnalysis.TempSpecificClasses;

namespace TemperatureAnalysis.Smoulder
{
    public class OutputDataSegment : IDistributeDataObject
    {
        public int Count;
        public decimal TemperatureSum;
        public int LastId;
        public Peak Peak;
    }
}
