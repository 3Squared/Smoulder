namespace Smoulder.Application.ConcreteClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            var smoulderFactory = new SmoulderFactory();
            var smoulder = smoulderFactory.Build(new Loader(), new Processor(), new Distributor());

            smoulder.Start();

            System.Threading.Thread.Sleep(2000);

            smoulder.Stop();
        }
    }
}
