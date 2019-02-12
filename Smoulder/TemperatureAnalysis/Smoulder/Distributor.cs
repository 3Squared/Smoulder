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
        private int _peakCount;
        private decimal _totalSum;
        private decimal _peakSum;
        private decimal _average;
        private decimal _averagePeak;
        private Peak _maxPeak = new Peak();
        private DateTime? _startDate = null;
        private DateTime _endDate = new DateTime();
        private int? _currentMonth = null;
        private int? _currentYear = null;

        private List<MonthData> Months = new List<MonthData>();

        private List<Peak> peaks = new List<Peak>();

        public override async void Action(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (DistributorQueue.TryDequeue(out var incomingData))
                {
                    var data = (OutputDataSegment) incomingData;

                    if (!_startDate.HasValue)
                    {
                        _startDate = data.Peak.Time.Date;
                    }

                    if (!_currentMonth.HasValue)
                    {
                        _currentMonth = data.Peak.Time.Month;
                        _currentYear = data.Peak.Time.Year;
                    }

                    if (data.Peak.Time.Date > _endDate)
                    {
                        _endDate = data.Peak.Time;
                    }

                    if (_currentMonth < data.Peak.Time.Month || _currentYear < data.Peak.Time.Year)
                    {
                        Months.Add(new MonthData
                        {
                            _average = _average,
                            _averagePeak = _averagePeak,
                            _endDate = _endDate,
                            _maxPeak = _maxPeak,
                            _startDate = _startDate,
                            _totalCount = _totalCount,
                            Month = _currentMonth.Value
                        });
                        _totalCount = 0;
                        _peakCount = 0;
                        _totalSum = 0;
                        _peakSum = 0;
                        _average = 0;
                        _averagePeak = 0;
                         _maxPeak = new Peak();
                        _startDate = null;
                        _endDate = new DateTime();
                        _currentMonth = data.Peak.Time.Month;
                        _currentYear = data.Peak.Time.Year;
                    }


                    _totalCount = _totalCount + data.Count;
                    _peakCount++;
                    _totalSum = _totalSum + data.TemperatureSum;
                    _peakSum = _peakSum + data.Peak.Temperature;
                    _average = _totalSum / _totalCount;
                    _averagePeak = _peakSum / _peakCount;
                    peaks.Add(data.Peak);

                    if (data.Peak.Temperature > _maxPeak.Temperature)
                    {
                        _maxPeak = data.Peak;
                    }

                    Console.WriteLine(
                        "{0}: PeakTime {1}, PeakTemp {2:0.0}, DailyAverage {3:0.0}, runningAverage {4:0.0}, runningMaxTemp {5:0.0}",
                        (data.Peak.Time.DayOfWeek + " - " + data.Peak.Time.ToShortDateString()).PadRight(22),
                        data.Peak.Time.TimeOfDay,
                        data.Peak.Temperature,
                        data.TemperatureSum / data.Count,
                        _average, _maxPeak.Temperature);
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }

        public override async Task Finalise()
        {
            Months.Add(new MonthData
            {
                _average = _average,
                _averagePeak = _averagePeak,
                _endDate = _endDate,
                _maxPeak = _maxPeak,
                _startDate = _startDate,
                _totalCount = _totalCount,
                Month = _currentMonth.Value
            });
            foreach (var monthData in Months)
            {
                Console.WriteLine("For Month {0}",monthData.Month);
                Console.WriteLine("Average Temperature during office hours {0:0.0}", monthData._average);
                Console.WriteLine("Average Peak Temperature during office hours {0:0.0}", monthData._averagePeak);
                Console.WriteLine("Max Temperature {0:0.0}", monthData._maxPeak.Temperature);
                Console.WriteLine("Hottest Day {0}", monthData._maxPeak.Time.ToShortDateString());
                Console.WriteLine("Number of Data points {0}", monthData._totalCount);
                Console.WriteLine("Start Date {0}", monthData._startDate?.ToShortDateString());
                Console.WriteLine("End Date {0}", monthData._endDate.ToShortDateString());
            }
        }

        private class MonthData
        {
            public int Month = 0;
            public decimal _average;
            public decimal _averagePeak;
            public Peak _maxPeak = new Peak();
            public int _totalCount;
            public DateTime? _startDate = null;
            public DateTime _endDate = new DateTime();
        }
    }
}
