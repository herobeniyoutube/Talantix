using Microsoft.Data.Sqlite;

namespace Talantix.ModelsLibrary.Interfaces
{
    public interface IDbProvider
    {
        public SqliteConnection OpenConnection();
    }
}
