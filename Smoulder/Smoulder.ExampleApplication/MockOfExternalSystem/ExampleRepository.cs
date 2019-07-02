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
           // Reset the database connection whenever smoulder is (re)started
        }

        public void Dispose()
        {
            //Close down the connection, sweep under the rug etc
        }

        public void CleanupDataInDownTime()
        {
            //Do some cleanup process in the downtime between items on the queue
        }
    }
}
