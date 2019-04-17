namespace Smoulder.Interfaces
{
    public interface ISmoulderFactory
    {
        Smoulder<TProcessData, TDistributeData> Build<TProcessData, TDistributeData>(ILoader<TProcessData> loader, IProcessor<TProcessData, TDistributeData> processor, IDistributor<TDistributeData> distributor, int processorQueueBound = 0, int distributorQueueBound = 0) where TProcessData : new() where TDistributeData : new();
    }
}
