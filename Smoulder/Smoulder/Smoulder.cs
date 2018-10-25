using Smoulder.Interfaces;

namespace Smoulder
{
    public class Smoulder : ISmoulder
    {
        private ILoader _loader;
        private IProcessor _processor;
        private IDistributor _distributor;

        public Smoulder(ILoader loader, IProcessor processor, IDistributor distributor)
        {
            _loader = loader;
            _processor = processor;
            _distributor = distributor;
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}
