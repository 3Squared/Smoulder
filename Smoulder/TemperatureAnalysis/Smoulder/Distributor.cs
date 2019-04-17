using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using TemperatureAnalysis.TempSpecificClasses;

namespace TemperatureAnalysis.Smoulder
{
    public class Distributor : DistributorBase<Day>
    {
        private List<Day> _daysInMonth = new List<Day>();
        private Day lastDay = null;
        private List<MonthData> _months = new List<MonthData>();

        public override void Action(Day data, CancellationToken cancellationToken)
        {
            var date = (data.Peak.Time.DayOfWeek + " - " + data.Peak.Time.ToShortDateString()).PadRight(22);
            Console.WriteLine(
                $"{date}: PeakTime {data.Peak.Time.TimeOfDay}, " +
                $"PeakTemp {data.Peak.Temperature:0.0}, " +
                $"DailyAverage {data.AverageTemp:0.0}, " +
                $"runningMaxTemp {_daysInMonth.OrderByDescending(d => d.Peak.Temperature).FirstOrDefault()?.Peak.Temperature:0.0}");

            if (lastDay != null && (lastDay.Peak.Time.Month < data.Peak.Time.Month || lastDay.Peak.Time.Year < data.Peak.Time.Year)
            ) //Current data is the last day of the Month
            {
                _months.Add(new MonthData
                {
                    Average = _daysInMonth.Average(d => d.AverageTemp),
                    AveragePeak = _daysInMonth.Average(d => d.Peak.Temperature),
                    EndDate = _daysInMonth.OrderBy(d => d.Date).Last().Date,
                    MaxPeak = _daysInMonth.OrderByDescending(d => d.Peak.Temperature).First().Peak,
                    Minimum = _daysInMonth.OrderBy(d => d.Minimum).First().Minimum,
                    StartDate = _daysInMonth.OrderBy(d => d.Date).First().Date,
                    TotalCount = _daysInMonth.Sum(d => d.Count),
                    Month = _daysInMonth.First().Date.Month,
                    Year = _daysInMonth.First().Date.Year
                });
                _daysInMonth = new List<Day>();
            }

            _daysInMonth.Add(data);
            lastDay = data;
        }

        public override void OnNoQueueItem(CancellationToken cancellationToken)
        {
            Thread.Sleep(500);
        }
        public override void OnError(Exception e)
        {
            Console.WriteLine("Error caught:" + e);
        }

        public override void Finalise()
        {
            _months.Add(new MonthData
            {
                Average = _daysInMonth.Average(d => d.AverageTemp),
                AveragePeak = _daysInMonth.Average(d => d.Peak.Temperature),
                EndDate = _daysInMonth.OrderBy(d => d.Date).Last().Date,
                MaxPeak = _daysInMonth.OrderByDescending(d => d.Peak.Temperature).First().Peak,
                Minimum = _daysInMonth.OrderBy(d => d.Minimum).First().Minimum,
                StartDate = _daysInMonth.OrderBy(d => d.Date).First().Date,
                TotalCount = _daysInMonth.Sum(d => d.Count),
                Month = _daysInMonth.First().Date.Month,
                Year = _daysInMonth.First().Date.Year
            });

            Console.WriteLine("");
            Console.WriteLine("----Output Date----");
            Console.WriteLine("");
            foreach (var monthData in _months)
            {
                Console.WriteLine($"Month {monthData.StartDate:MMMM}, year {monthData.StartDate.Value.Year}");
                Console.WriteLine($"Average Temperature during office hours {monthData.Average:0.0}");
                Console.WriteLine($"Average Peak Temperature during office hours {monthData.AveragePeak:0.0}");
                Console.WriteLine($"Max Temperature {monthData.MaxPeak.Temperature:0.0}");
                Console.WriteLine($"Min Temperature {monthData.Minimum:0.0}");
                Console.WriteLine($"Hottest Day {monthData.MaxPeak.Time.ToShortDateString()}");
                Console.WriteLine($"Number of Data points {monthData.TotalCount}");
                Console.WriteLine($"Start Date {monthData.StartDate?.ToShortDateString()}");
                Console.WriteLine($"End Date {monthData.EndDate.ToShortDateString()}");
                Console.WriteLine("");
            }

        }

        private class MonthData
        {
            public int Month = 0;
            public int Year = 0;
            public double Average;
            public double AveragePeak;
            public Peak MaxPeak = new Peak();
            public double Minimum;
            public int TotalCount;
            public DateTime? StartDate = null;
            public DateTime EndDate = new DateTime();
        }
    }
}


