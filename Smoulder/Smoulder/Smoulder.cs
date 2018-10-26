using System.Collections.Concurrent;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class Smoulder : ISmoulder
    {
        private ILoader _loader;
        private IProcessor _processor;
        private IDistributor _distributor;
        private ConcurrentQueue<IProcessDataObject> _processorQueue;
        private ConcurrentQueue<IDistributeDataObject> _distributorQueue;

        public bool IsRunning;
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
        }

        public async void Start()
        {
            var loaderStartTask = Task.Factory.StartNew(() => _loader.Start());
            var processorStartTask = Task.Factory.StartNew(() => _processor.Start());
            var distributorStartTask = Task.Factory.StartNew(() => _distributor.Start());

            IsRunning = true;

            await loaderStartTask;
            await processorStartTask;
            await distributorStartTask;
        }

        public async void Stop()
        {
            var loaderStopTask = Task.Factory.StartNew(() => _loader.Stop());
            var processorStopTask = Task.Factory.StartNew(() => _processor.Stop());
            var distributorStopTask = Task.Factory.StartNew(() => _distributor.Stop());

            await loaderStopTask;
            await processorStopTask;
            await distributorStopTask;

            IsRunning = false;
        }

    }
}
