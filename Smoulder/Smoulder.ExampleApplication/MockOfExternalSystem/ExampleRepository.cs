using System;

namespace Smoulder.ExampleApplication
{
    public class ExampleRepository : IExampleRepository, IDisposable
    {
        public ExampleRepository()
        {
            
        }

        public void SaveData(ProcessDataObject data)
        {
           // Save some data to the database 
        }

        public void ResetConnection()
        {
           // Save some data to the database 
        }

        public void Dispose()
        {
            //Close down a connection
        }
    }
}
