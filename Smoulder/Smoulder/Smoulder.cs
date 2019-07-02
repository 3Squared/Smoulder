using System;
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
        private readonly BlockingCollection<TProcessData> _processorQueue;
        private readonly BlockingCollection<TDistributeData> _distributorQueue;

        private CancellationTokenSource _loaderCancellationTokenSource;
        private CancellationTokenSource _processorCancellationTokenSource;
        private CancellationTokenSource _distributorCancellationTokenSource;

        public int ProcessorQueueItems => _processorQueue.Count;
        public int DistributorQueueItems => _distributorQueue.Count;

        public Smoulder(ILoader<TProcessData> loader, IProcessor<TProcessData, TDistributeData> processor, IDistributor<TDistributeData> distributor,
            BlockingCollection<TProcessData> processorQueue, BlockingCollection<TDistributeData> distributorQueue)
        {
            _loader = loader;
            _processor = processor;
            _distributor = distributor;
            _processorQueue = processorQueue;
            _distributorQueue = distributorQueue;
        }

        public void Start()
        {
            lock (this)
            {
                _loaderCancellationTokenSource = new CancellationTokenSource();
                _processorCancellationTokenSource = new CancellationTokenSource();
                _distributorCancellationTokenSource = new CancellationTokenSource();
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

        public Smoulder<TProcessData, TDistributeData> SetLoaderAction(Func<CancellationToken, TProcessData> action)
        {
            _loader.SetAction(action);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetLoaderFinalise(Action finalise)
        {
            _loader.SetFinalise(finalise);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetLoaderOnError(Action<Exception> onError)
        {
            _loader.SetOnError(onError);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetLoaderStartup(Action startup)
        {
            _loader.SetStartup(startup);
            return this;
        }

        public Smoulder<TProcessData, TDistributeData> SetProcessorAction(Func<TProcessData, CancellationToken, TDistributeData> action)
        {
            _processor.SetAction(action);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetProcessorOnEmptyQueue(Action<CancellationToken> onEmptyQueue)
        {
            _processor.SetOnEmptyQueue(onEmptyQueue);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetProcessorFinalise(Action finalise)
        {
            _processor.SetFinalise(finalise);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetProcessorOnError(Action<Exception> onError)
        {
            _processor.SetOnError(onError);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetProcessorStartup(Action startup)
        {
            _processor.SetStartup(startup);
            return this;
        }

        public Smoulder<TProcessData, TDistributeData> SetDistributorAction(Action<TDistributeData, CancellationToken> action)
        {
            _distributor.SetAction(action);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetDistributorOnEmptyQueue(Action<CancellationToken> onEmptyQueue)
        {
            _distributor.SetOnEmptyQueue(onEmptyQueue);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetDistributorFinalise(Action finalise)
        {
            _distributor.SetFinalise(finalise);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetDistributorOnError(Action<Exception> onError)
        {
            _distributor.SetOnError(onError);
            return this;
        }
        public Smoulder<TProcessData, TDistributeData> SetDistributorStartup(Action startup)
        {
            _distributor.SetStartup(startup);
            return this;
        }
    }
}
