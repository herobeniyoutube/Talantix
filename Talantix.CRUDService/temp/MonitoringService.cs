using Microsoft.Data.Sqlite;

namespace Talantix.WebApi.temp
{
    public class MonitoringService
    {
        public MonitoringService()
        {

        }
        public List<SqliteConnection> Connections { get; set; } = new List<SqliteConnection>();
        public void CheckIsAlive()
        {
            
        }
    }
}
