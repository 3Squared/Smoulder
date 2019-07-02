using Smoulder.ExampleApplication.SmoulderClasses;

namespace Smoulder.ExampleApplication.MockOfExternalSystem
{
    public interface IExampleRepository
    {
        void SaveData(ProcessDataObject data);
        void ResetConnection();
    }
}