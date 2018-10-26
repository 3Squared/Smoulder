using System.Threading;

namespace Smoulder.Concrete
{
    public class ConcreteLoader : LoaderBase
    {
        public override void Action(CancellationToken cancellationToken)
        {
            var data = new ConcreteProcessorDataObject();
            ProcessorQueue.Enqueue(data);
        }
    }
}
