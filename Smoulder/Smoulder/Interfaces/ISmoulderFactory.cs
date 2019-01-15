namespace Smoulder.Interfaces
{
    public interface ISmoulderFactory
    {
        Smoulder Build(ILoader loader, IProcessor processor, IDistributor distributor);
    }
}
