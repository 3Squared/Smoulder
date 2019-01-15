using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using Smoulder.Interfaces;
using TrainDataListener.Repository;
using TrainDataListener.TrainData;

namespace TrainDataListener.Smoulder
{
    public class Distributor : DistributorBase
    {
        private ScheduleRepository _scheduleRepository;
        private int _flushCycles;
        private int unflushedCycles = 0;

        public override async void Action(CancellationToken cancellationToken)
        {
            if (DistributorQueue.TryDequeue(out var incomingData))
            {
                var trustMessage = (TrustMessage)incomingData;
                WriteToConsole(unflushedCycles + " - " + trustMessage.MessageType.ToString());
                _scheduleRepository.ProcessSchedule(trustMessage);
            }
            else
            {
                Thread.Sleep(500);
            }

            if (unflushedCycles >= _flushCycles)
            {
                var cleanedRows = _scheduleRepository.CleanUp();
                WriteToConsole($"{cleanedRows} removed");
                unflushedCycles = 0;
            }
            unflushedCycles++;
        }

        private void WriteToConsole(string output)
        {
            Console.WriteLine($"Distributor - {DateTime.Now} " + output);
        }

        public override async Task Startup(IStartupParameters startupParameters)
        {
            _flushCycles = 100;
            _scheduleRepository = new ScheduleRepository();
            var flushedRows = _scheduleRepository.CleanUp();
            WriteToConsole($"{flushedRows} flushed by distributor on startup");
        }
    }
}
