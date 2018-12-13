using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;
using Smoulder.Interfaces;

namespace TrainDataListener.Smoulder
{
    public class Processor : ProcessorBase
    {
        private decimal _lastCount;
        private DateTime _lastTime;
        public override async void Action(CancellationToken cancellationToken)
        {
            WriteToConsole($"count - {ProcessorQueue.Count} messages");
            var deltaCount = ProcessorQueue.Count - _lastCount;
            var deltaTime = (decimal) (DateTime.Now - _lastTime).TotalSeconds;
            var rate = Math.Round(deltaCount / deltaTime);
            WriteToConsole($"rate - {rate} messages per second");
            _lastCount = ProcessorQueue.Count;
            _lastTime = DateTime.Now;

            Thread.Sleep(1000);
        }
        
        private void WriteToConsole(string output)
        {
            Console.WriteLine("Processor - " + output);
        }

        public override async Task Startup(IStartupParameters startupParameters)
        {
            _lastCount = 0;
            _lastTime = DateTime.Now;
        }
    }
}
