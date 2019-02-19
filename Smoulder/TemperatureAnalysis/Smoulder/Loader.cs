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
        public override async Task Action(CancellationToken cancellationToken)
        {
            if (_finished)
            {
                await Task.Delay(1000);
                return;
            }
            using (var reader = new StreamReader(@"TemperatureData.csv"))
            {
                const DayOfWeek startOfWeek = DayOfWeek.Monday;
                const DayOfWeek endOfWeek = DayOfWeek.Friday;
                var startOfDay = new TimeSpan(8, 0, 0);
                var endOfDay = new TimeSpan(18, 0, 0);

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

                    if (potentialTempData.Time.DayOfWeek >= startOfWeek &&
                        potentialTempData.Time.DayOfWeek <= endOfWeek &&
                        (potentialTempData.Time.TimeOfDay >= startOfDay &&
                         potentialTempData.Time.TimeOfDay <= endOfDay))
                    {
                        ProcessorQueue.Enqueue(potentialTempData);
                    }
                }
                _finished = true;
            }
        }

    }
}
