using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using TemperatureAnalysis.TempSpecificClasses;

namespace TemperatureAnalysis.Smoulder
{
    public class Distributor : DistributorBase
    {
        private int _totalCount;
        private decimal _totalSum;
        private decimal _average;

        private List<Peak> peaks = new List<Peak>();

        public override async void Action(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (DistributorQueue.TryDequeue(out var incomingData))
                {
                    var data = (OutputDataSegment) incomingData;
                    _totalCount = _totalCount + data.Count;
                    _totalSum = _totalSum + data.TemperatureSum;
                    _average = _totalSum / _totalCount;
                    peaks.Add(data.Peak);
                    foreach (var item in peaks)
                        Console.Write("{0} ,", item.Time);
                    Console.WriteLine();

                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }
    }
}
