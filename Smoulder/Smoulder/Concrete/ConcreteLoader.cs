namespace Smoulder.Concrete
{
    public class ConcreteLoader : LoaderBase
    {
        public override void Action()
        {
            var data = new ConcreteProcessorDataObject();
            ProcessorQueue.Enqueue(data);
        }
    }
}
