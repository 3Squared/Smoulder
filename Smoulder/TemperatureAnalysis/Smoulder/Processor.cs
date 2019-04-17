using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Smoulder;
using TemperatureAnalysis.TempSpecificClasses;

namespace TemperatureAnalysis.Smoulder
{
    public class Processor : ProcessorBase<LoadedTempData, Day>
    {
        List<LoadedTempData> _dayData = new List<LoadedTempData>();

        public override void Action(LoadedTempData data, CancellationToken cancellationToken)
        {
            _dayData.Add(data);

            if (Peek(out var peekedData) && peekedData.Time.Date > data.Time.Date
            ) //Current data is the last measurement of the day
            {
                var peak = _dayData.OrderBy(x => x.Temperature).Last();

                Enqueue(new Day
                {
                    Count = _dayData.Count,
                    AverageTemp = _dayData.Sum(ltd => ltd.Temperature) / _dayData.Count,
                    Peak = new Peak
                    {
                        Id = peak.Id,
                        Temperature = peak.Temperature,
                        Time = peak.Time
                    },
                    Minimum = _dayData.OrderBy(x => x.Temperature).First().Temperature,
                    Date = data.Time.Date

                });
                _dayData = new List<LoadedTempData>();
            }
        }

        public override void OnNoQueueItem(CancellationToken cancellationToken)
        {
            Thread.Sleep(500);
        }
    }
}
