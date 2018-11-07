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

        public override async void Action(CancellationToken cancellationToken)
        {
            var runningArray = new List<LoadedTempData>();
            var data = new LoadedTempData { Id = -1 };

            while (!cancellationToken.IsCancellationRequested)
            {
                if (ProcessorQueue.TryDequeue(out var incomingData))
                {
                    data = (LoadedTempData) incomingData;
                    runningArray.Add(data);

                    var peakedData = new LoadedTempData();
                    if (ProcessorQueue.TryPeek(out var incomingPeakedData))
                    {
                        peakedData = (LoadedTempData)incomingPeakedData;
                    }

                    if (peakedData.Time.Date > data.Time.Date)
                    {
                        _count = runningArray.Count;
                        _temperatureSum = runningArray.Sum(x => x.Temperature);
                        var peak = runningArray.OrderBy(x => x.Temperature).Last();

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
                        break;
                    }

                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }
    }
}
