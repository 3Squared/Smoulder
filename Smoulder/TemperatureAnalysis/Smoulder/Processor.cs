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
    public class Processor : ProcessorBase<LoadedTempData, Day>
    {
        List<LoadedTempData> dayData = new List<LoadedTempData>();

        public override void Action(CancellationToken cancellationToken)
        {
            var data = new LoadedTempData {Id = -1};
                data = Dequeue();
                dayData.Add(data);

            var peekedData = Peek();
                if (peekedData != null && peekedData.Time.Date > data.Time.Date) //Current data is the last measurement of the day
                {
                    var peak = dayData.OrderBy(x => x.Temperature).Last();

                    Enqueue(new Day
                    {
                        Count = dayData.Count,
                        AverageTemp = dayData.Sum(ltd => ltd.Temperature) / dayData.Count,
                        Peak = new Peak
                        {
                            Id = peak.Id,
                            Temperature = peak.Temperature,
                            Time = peak.Time
                        },
                        Minimum = dayData.OrderBy(x => x.Temperature).First().Temperature,
                        Date = data.Time.Date

                    });
                    dayData = new List<LoadedTempData>();
                }
        }

        public override void OnNoQueueItem(CancellationToken cancellationToken)
        {
            Thread.Sleep(500);
        }
    }
}
