using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class Smoulder<TProcessData, TDistributeData> : ISmoulder where TProcessData : new() where TDistributeData : new()
    {
        private readonly ILoader<TProcessData> _loader;
        private readonly IProcessor<TProcessData, TDistributeData> _processor;
        private readonly IDistributor<TDistributeData> _distributor;
        private readonly ConcurrentQueue<TProcessData> _processorQueue;
        private readonly ConcurrentQueue<TDistributeData> _distributorQueue;

        private readonly CancellationTokenSource _loaderCancellationTokenSource;
        private readonly CancellationTokenSource _processorCancellationTokenSource;
        private readonly CancellationTokenSource _distributorCancellationTokenSource;

        public int ProcessorQueueItems => _processorQueue.Count;
        public int DistributorQueueItems => _distributorQueue.Count;

        public Smoulder(ILoader<TProcessData> loader, IProcessor<TProcessData, TDistributeData> processor, IDistributor<TDistributeData> distributor,
            ConcurrentQueue<TProcessData> processorQueue, ConcurrentQueue<TDistributeData> distributorQueue)
        {
            _loader = loader;
            _processor = processor;
            _distributor = distributor;
            _processorQueue = processorQueue;
            _distributorQueue = distributorQueue;

            _loaderCancellationTokenSource = new CancellationTokenSource();
            _processorCancellationTokenSource = new CancellationTokenSource();
            _distributorCancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            lock (this)
            {
                Task.Factory.StartNew(() => _loader.Start(_loaderCancellationTokenSource.Token));
                Task.Factory.StartNew(() => _processor.Start(_processorCancellationTokenSource.Token));
                Task.Factory.StartNew(() => _distributor.Start(_distributorCancellationTokenSource.Token));
            }
        }

        public void Stop()
        {
            lock (this)
            {
                _loaderCancellationTokenSource.Cancel();
                _loader.Finalise();
                _processorCancellationTokenSource.Cancel();
                _processor.Finalise();
                _distributorCancellationTokenSource.Cancel();
                _distributor.Finalise();
            }
        }
    }
}
