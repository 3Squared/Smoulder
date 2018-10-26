namespace Smoulder.Interfaces
{
    internal interface ISmoulderFactory
    {
        Smoulder Build(ILoader loader, IProcessor processor, IDistributor distributor);
    }
}
