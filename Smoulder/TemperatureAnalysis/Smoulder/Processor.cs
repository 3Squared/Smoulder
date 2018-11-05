using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using Smoulder.Interfaces;
using TemperatureAnalysis.TempSpecificClasses;

namespace TemperatureAnalysis.Smoulder
{
    public class Processor : ProcessorBase
    {
        private int _count;
        private decimal _temperatureSum;
        private DateTime _currentDate = DateTime.MinValue;

        public override async void Action(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var runningArray = new List<LoadedTempData>();
                var peaks = new List<Peak>();
                var data = new LoadedTempData{Id = -1};

                if (ProcessorQueue.TryPeek(out var incomingPeakedData))
                {
                    var peakedData = (LoadedTempData)incomingPeakedData;
                    _currentDate = peakedData.Time;
                }
                else
                {
                    break;
                }

                while (data.Time.Date <= _currentDate)
                {
                    if (ProcessorQueue.TryDequeue(out var incomingData))
                    {
                        data = (LoadedTempData) incomingData;
                        runningArray.Add(data);
                    }
                    else
                    {
                        await Task.Delay(500);
                    }
                }

                _count = runningArray.Count;
                _temperatureSum = runningArray.Sum(x => x.Temperature);
                var peak = runningArray.OrderBy(x => x.Temperature).First();

                DistributorQueue.Enqueue(new OutputDataSegment
                {
                    Count = _count,
                    TemperatureSum = _temperatureSum,
                    LastId = runningArray.Last().Id,
                    Peak = new Peak
                    {
                        Id = peak.Id,
                        Temperature = peak.Temperature,
                        Time = peak.Time
                    }
                });
            }
        }
    }
}
