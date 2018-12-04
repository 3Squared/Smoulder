using System.Collections.Concurrent;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class SmoulderFactory : ISmoulderFactory
    {
        public Smoulder Build(ILoader loader, IProcessor processor, IDistributor distributor)
        {
            //Create Queues
            ConcurrentQueue<IProcessDataObject> processorQueue = new ConcurrentQueue<IProcessDataObject>();
            ConcurrentQueue<IDistributeDataObject> distributorQueue = new ConcurrentQueue<IDistributeDataObject>();

            //Hooks units up to Queues
            loader.RegisterProcessorQueue(processorQueue);
            processor.RegisterProcessorQueue(processorQueue);

            processor.RegisterDistributorQueue(distributorQueue);
            distributor.RegisterDistributorQueue(distributorQueue);

            //Creates a Smoulder encapsulating the units
            var smoulder = new Smoulder(loader, processor, distributor, processorQueue, distributorQueue);
            //Returns the Smoulder
            return smoulder;
        }
    }
}
