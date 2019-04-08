using System;
using System.Collections.Generic;
using Smoulder.Interfaces;
using TemperatureAnalysis.TempSpecificClasses;

namespace TemperatureAnalysis.Smoulder
{
    public class Day : IDistributeDataObject
    {
        public int Count;
        public double AverageTemp;
        public Peak Peak;
        public double Minimum;
        public DateTime Date;
    }
}
