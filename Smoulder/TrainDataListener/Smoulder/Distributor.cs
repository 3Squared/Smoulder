using System;
using System.Threading;
using System.Threading.Tasks;
using Smoulder;

namespace TrainDataListener.Smoulder
{
    public class Distributor : DistributorBase
    {
        public override async void Action(CancellationToken cancellationToken)
        {
            WriteToConsole($"");
            Thread.Sleep(1000);
        }

        private void WriteToConsole(string output)
        {
            Console.WriteLine("Distributor - " + output);
        }
    }
}
