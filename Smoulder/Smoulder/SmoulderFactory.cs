using Smoulder.Interfaces;

namespace Smoulder
{
    public class SmoulderFactory : ISmoulderFactory
    {
        public Smoulder Build(ILoader loader, IProcessor processor, IDistributor distributor)
        {
            throw new System.NotImplementedException();
        }
    }
}
