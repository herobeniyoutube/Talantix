using Microsoft.Data.Sqlite;
using Talantix.ModelsLibrary.Interfaces;

namespace Talantix.Repository
{
    public class DbProvider : IDbProvider
    {
        public DbProvider()
        {
            if (!File.Exists("ToDoApp.db")) InitialCreation();
        }

        private SqliteConnection connection = new SqliteConnection("Data Source=ToDoApp.db");

        public SqliteConnection OpenConnection()
        {
            connection.Open();

            return connection;
        }

        private void InitialCreation()
        {
            using (var db = OpenConnection())
            {
                var command = new SqliteCommand()
                {
                    CommandText = @"
                                    CREATE table ToDoList 
                                    (
                                        id integer default 0 primary key autoincrement,
                                        Name varchar(100) not null,
                                        Description varchar(500),
                                        CreationDate datetime not null,
                                        IsComplete bit default 1
                                    )",
                    Connection = db
                };
                var command2 = new SqliteCommand()
                {
                    CommandText = @"
                                    CREATE table RequestsAnalitycs
                                    (
                                        Id integer default 0 primary key autoincrement,
                                        TaskId integer null,
                                        IP varchar(20) not null,
                                        RequestTime datetime not null,
                                        RequestPath varchar(100) not null,
                                        Foreign key (TaskId) References ToDoList (Id) on delete set null
                                    )",
                    Connection = db

                };

                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
            }
        }
    }
}
