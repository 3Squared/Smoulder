namespace Smoulder.Interfaces
{
    public interface ISmoulderFactory
    {
        Smoulder<TProcessData, TDistributeData> Build<TProcessData, TDistributeData>(ILoader<TProcessData> loader, IProcessor<TProcessData, TDistributeData> processor, IDistributor<TDistributeData> distributor);
    }
}
