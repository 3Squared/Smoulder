namespace Smoulder.ExampleApplication
{
    public interface IExampleRepository
    {
        void SaveData(ProcessDataObject data);
        void ResetConnection();
    }
}