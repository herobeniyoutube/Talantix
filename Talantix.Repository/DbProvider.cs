using Microsoft.Data.Sqlite;
using Talantix.ModelsLibrary.Interfaces;

namespace Talantix.Repository
{
    public class DbProvider : IDbProvider, IDisposable
    {
        private SqliteConnection connection = new SqliteConnection("Data Source=ToDoApp.db");

        public SqliteConnection OpenConnection() 
        {
            connection.Open();
            
            return connection;
        }

        public void Dispose()
        {
            connection.Close();
            connection.Dispose();
        }
    }
}
