using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class WorkerUnitBase : IWorkerUnit
    {
        public bool ShouldStop = false;

        public async Task Stop()
        {
            ShouldStop = true;
        }

        public async Task Start()
        {
            while (!ShouldStop)
            {
                await Task.Run(() => Action());
            }
        }

        public virtual async void Action()
        {
            await Task.Delay(100);
        }
    }
}
