using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using Smoulder.Interfaces;
using TrainDataListener.Repository;
using TrainDataListener.TrainData;

namespace TrainDataListener.Smoulder
{
    public class Processor : ProcessorBase
    {
        private DateTime _lastFlush;
        private MovementRepository _movementRepository;
        private int _flushCycles;
        private int unflushedCycles = 0;
        private int unflushedRows = 0;

        public override async void Action(CancellationToken cancellationToken)
        {
            if (ProcessorQueue.TryDequeue(out var incomingData))
            {
                var trustMessage = (TrustMessage) incomingData;

                _movementRepository.AddTrustMessage(trustMessage);
                unflushedRows++;
            }
            else
            {
                Thread.Sleep(10);
            }

            if (unflushedCycles >= _flushCycles)
            {
                var secondsSinceLastFlush = (DateTime.Now - _lastFlush).TotalSeconds;
                WriteToConsole(
                    $"Flushing after {unflushedCycles} cycles in {secondsSinceLastFlush} seconds; rate {Math.Round(unflushedRows / secondsSinceLastFlush)} messages/second");
                _movementRepository.Flush();
                unflushedCycles = 0;
                unflushedRows = 0;
                _lastFlush = DateTime.Now;
                
            }
            unflushedCycles++;
        }
        
        private void WriteToConsole(string output)
        {
            Console.WriteLine("Processor - " + output);
        }

        public override async Task Startup(IStartupParameters startupParameters)
        {
            _lastFlush = DateTime.Now;
            _flushCycles = 100;
            _movementRepository = new MovementRepository();
        }
    }
}
