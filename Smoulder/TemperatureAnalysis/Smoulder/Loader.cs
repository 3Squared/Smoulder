using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;

namespace TemperatureAnalysis.Smoulder
{
    public class Loader : LoaderBase
    {
        private bool _finished = false;
        private const DayOfWeek StartOfWeek = DayOfWeek.Monday;
        private const DayOfWeek EndOfWeek = DayOfWeek.Friday;
        private readonly TimeSpan _startOfDay = new TimeSpan(8, 0, 0);
        private readonly TimeSpan _endOfDay = new TimeSpan(18, 0, 0);

        public override async Task Action(CancellationToken cancellationToken)
        {
            if (_finished)
            {
                await Task.Delay(1000);
                return;
            }
            using (var reader = new StreamReader(@"TemperatureData.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var potentialTempData = new LoadedTempData
                    {
                        Id = int.Parse(values[0]),
                        Time = DateTime.Parse(values[1]),
                        Temperature = decimal.Parse(values[2])

                    };

                    if (potentialTempData.Time.DayOfWeek >= StartOfWeek &&
                        potentialTempData.Time.DayOfWeek <= EndOfWeek &&
                        (potentialTempData.Time.TimeOfDay >= _startOfDay &&
                         potentialTempData.Time.TimeOfDay <= _endOfDay))
                    {
                        ProcessorQueue.Enqueue(potentialTempData);
                    }
                }
                _finished = true;
            }
        }

    }
}
