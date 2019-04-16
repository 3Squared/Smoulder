using System.Collections.Concurrent;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class SmoulderFactory : ISmoulderFactory
    {
        public Smoulder<TProcessData, TDistributeData> Build<TProcessData, TDistributeData>(
            ILoader<TProcessData> loader,
            IProcessor<TProcessData, TDistributeData> processor,
            IDistributor<TDistributeData> distributor) where TProcessData : new() where TDistributeData : new()
        {
            //Create Queues
            ConcurrentQueue<TProcessData> processorQueue = new ConcurrentQueue<TProcessData>();
            ConcurrentQueue<TDistributeData> distributorQueue = new ConcurrentQueue<TDistributeData>();

            //Hooks units up to Queues
            loader.RegisterProcessorQueue(processorQueue);
            processor.RegisterProcessorQueue(processorQueue);

            processor.RegisterDistributorQueue(distributorQueue);
            distributor.RegisterDistributorQueue(distributorQueue);

            //Creates a Smoulder encapsulating the units
            var smoulder =
                new Smoulder<TProcessData, TDistributeData>(loader, processor, distributor, processorQueue,
                    distributorQueue);
            //Returns the Smoulder
            return smoulder;
        }
    }
}
