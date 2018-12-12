using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class Smoulder : ISmoulder
    {
        private readonly ILoader _loader;
        private readonly IProcessor _processor;
        private readonly IDistributor _distributor;
        private readonly ConcurrentQueue<IProcessDataObject> _processorQueue;
        private readonly ConcurrentQueue<IDistributeDataObject> _distributorQueue;

        public readonly CancellationTokenSource loaderCancellationTokenSource;
        public readonly CancellationTokenSource ProcessorCancellationTokenSource;
        public readonly CancellationTokenSource DistributorCancellationTokenSource;

        public int ProcessorQueueItems => _processorQueue.Count;
        public int DistributorQueueItems => _distributorQueue.Count;

        public Smoulder(ILoader loader, IProcessor processor, IDistributor distributor,
            ConcurrentQueue<IProcessDataObject> processorQueue, ConcurrentQueue<IDistributeDataObject> distributorQueue)
        {
            _loader = loader;
            _processor = processor;
            _distributor = distributor;
            _processorQueue = processorQueue;
            _distributorQueue = distributorQueue;

            loaderCancellationTokenSource = new CancellationTokenSource();
            ProcessorCancellationTokenSource = new CancellationTokenSource();
            DistributorCancellationTokenSource = new CancellationTokenSource();
        }

        public async Task Start(params object[] args)
        {
            lock (this)
            {
                Task.Factory.StartNew(() => _loader.Start(loaderCancellationTokenSource.Token, args));
                Task.Factory.StartNew(() => _processor.Start(ProcessorCancellationTokenSource.Token, args));
                Task.Factory.StartNew(() => _distributor.Start(DistributorCancellationTokenSource.Token, args));
            }
        }

        public async Task Stop()
        {
            Console.WriteLine("Shutdown loader Starting");
            loaderCancellationTokenSource.Cancel();
            var loaderStopTask = Task.Factory.StartNew(() => _loader.Finalise());
            await loaderStopTask;

            Console.WriteLine("Shutdown processor Starting");
            ProcessorCancellationTokenSource.Cancel();
            var processorStopTask = Task.Factory.StartNew(() => _processor.Finalise());
            await processorStopTask;

            Console.WriteLine("Shutdown distributor Starting");
            DistributorCancellationTokenSource.Cancel();
            var distributorStopTask = Task.Factory.StartNew(() => _distributor.Finalise());
            await distributorStopTask;

            Console.WriteLine("Shutdown Complete");
        }
    }
}
