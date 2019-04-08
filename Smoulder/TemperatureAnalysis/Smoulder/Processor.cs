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
        public override async Task Action(CancellationToken cancellationToken)
        {
            var dayData = new List<LoadedTempData>();
            var data = new LoadedTempData { Id = -1 };

            while (!cancellationToken.IsCancellationRequested)
            {
                if (ProcessorQueue.TryDequeue(out var incomingData))
                {
                    data = (LoadedTempData) incomingData;
                    dayData.Add(data);

                    var peekedData = new LoadedTempData();
                    if (ProcessorQueue.TryPeek(out var incomingPeakedData))
                    {
                        peekedData = (LoadedTempData)incomingPeakedData;
                    }

                    if (peekedData.Time.Date > data.Time.Date) //Current data is the last measurement of the day
                    {
                        var peak = dayData.OrderBy(x => x.Temperature).Last();

                        DistributorQueue.Enqueue(new Day
                        {
                            Count = dayData.Count,
                            AverageTemp = dayData.Sum(ltd => ltd.Temperature)/ dayData.Count,
                            Peak = new Peak
                            {
                                Id = peak.Id,
                                Temperature = peak.Temperature,
                                Time = peak.Time
                            },
                            Minimum = dayData.OrderBy(x => x.Temperature).First().Temperature,
                            Date = data.Time.Date

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
