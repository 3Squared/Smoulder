using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class SmoulderFactory : ISmoulderFactory
    {
        private List<ConcurrentQueue<IDataObject>> _queues;

        public Smoulder Build(List<IWorkerUnit> workerUnits)
        {
            //Create Queues
            var numberOfQueues = workerUnits.Count - 1;

            _queues = new List<ConcurrentQueue<IDataObject>>();

            for (var i = 0; i < numberOfQueues; i++)
            {
                _queues.Add(new ConcurrentQueue<IDataObject>());
            }

            foreach (var workerUnit in workerUnits)
            {
                if (workerUnits.IndexOf(workerUnit) == 0)
                {
                    //Only connect downstream
                }

                //Connect both

                if (workerUnits.Last() == workerUnit)
                {
                    //Only connect upstream
                }
            }

            //Creates a Smoulder encapsulating the units
            var smoulder = new Smoulder();
            //Returns the Smoulder
            return smoulder;
        }
    }
}
