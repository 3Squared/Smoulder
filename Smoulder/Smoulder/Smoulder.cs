using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class Smoulder : ISmoulder
    {
        private readonly ConcurrentQueue<IDataObject> _processorQueue;
        private readonly ConcurrentQueue<IDataObject> _distributorQueue;

        private readonly CancellationTokenSource loaderCancellationTokenSource;
        private readonly CancellationTokenSource processorCancellationTokenSource;
        private readonly CancellationTokenSource distributorCancellationTokenSource;

        public int ProcessorQueueItems => _processorQueue.Count;
        public int DistributorQueueItems => _distributorQueue.Count;

        public Smoulder()
        {
            loaderCancellationTokenSource = new CancellationTokenSource();
            processorCancellationTokenSource = new CancellationTokenSource();
            distributorCancellationTokenSource = new CancellationTokenSource();
        }

        public async Task Start()
        {
            lock (this)
            {
                Task.Factory.StartNew(() => _loader.Start(loaderCancellationTokenSource.Token));
                Task.Factory.StartNew(() => _processor.Start(processorCancellationTokenSource.Token));
                Task.Factory.StartNew(() => _distributor.Start(distributorCancellationTokenSource.Token));
            }
        }

        public async Task Stop()
        {
            Console.WriteLine("Shutdown loader Starting");
            loaderCancellationTokenSource.Cancel();
            var loaderStopTask = Task.Factory.StartNew(() => _loader.Finalise());
            await loaderStopTask;

            Console.WriteLine("Shutdown processor Starting");
            processorCancellationTokenSource.Cancel();
            var processorStopTask = Task.Factory.StartNew(() => _processor.Finalise());
            await processorStopTask;

            Console.WriteLine("Shutdown distributor Starting");
            distributorCancellationTokenSource.Cancel();
            var distributorStopTask = Task.Factory.StartNew(() => _distributor.Finalise());
            await distributorStopTask;

            Console.WriteLine("Shutdown Complete");
        }
    }
}
