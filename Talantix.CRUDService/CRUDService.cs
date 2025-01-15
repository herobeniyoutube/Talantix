using Talantix.ModelsLibrary.Interfaces;
using Talantix.WebApi.temp;

namespace Talantix.Repository
{
    public class CRUDService : ICRUDService
    {
        public CRUDService(IDbProvider dbProvider, MonitoringService ms) 
        { 
            this.dbProvider = dbProvider;
            this.ms = ms;
        }

        private readonly IDbProvider dbProvider;
        private readonly MonitoringService ms;

        public async Task AddNewTask()
        {
            var db = dbProvider.OpenConnection();
            ms.Connections.Add(db);
             
        }
    }
}