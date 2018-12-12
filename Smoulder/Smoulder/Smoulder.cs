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

        public readonly CancellationTokenSource LoaderCancellationTokenSource;
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

            LoaderCancellationTokenSource = new CancellationTokenSource();
            ProcessorCancellationTokenSource = new CancellationTokenSource();
            DistributorCancellationTokenSource = new CancellationTokenSource();
        }

        public async Task Start(IStartupParameters startupParameters)
        {
            lock (this)
            {
                Task.Factory.StartNew(() => _loader.Start(LoaderCancellationTokenSource.Token, startupParameters));
                Task.Factory.StartNew(() => _processor.Start(ProcessorCancellationTokenSource.Token, startupParameters));
                Task.Factory.StartNew(() => _distributor.Start(DistributorCancellationTokenSource.Token, startupParameters));
            }
        }

        public async Task Start()
        {
            lock (this)
            {
                Task.Factory.StartNew(() => _loader.Start(LoaderCancellationTokenSource.Token));
                Task.Factory.StartNew(() => _processor.Start(ProcessorCancellationTokenSource.Token));
                Task.Factory.StartNew(() => _distributor.Start(DistributorCancellationTokenSource.Token));
            }
        }

        public async Task Stop()
        {
            lock (this)
            {
                LoaderCancellationTokenSource.Cancel();
                Task.Factory.StartNew(() => _loader.Finalise());
                ProcessorCancellationTokenSource.Cancel();
                Task.Factory.StartNew(() => _processor.Finalise());
                DistributorCancellationTokenSource.Cancel();
                Task.Factory.StartNew(() => _distributor.Finalise());
            }
        }
    }
}
