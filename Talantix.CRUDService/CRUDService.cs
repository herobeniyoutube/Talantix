using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Talantix.Application;
using Talantix.ModelsLibrary.Interfaces;
using Talantix.ModelsLibrary.Models;

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

        public async Task<int> AddNewTask(TodoItem item)
        {
            using (var db = dbProvider.OpenConnection())
            {
                var param = new List<SqliteParameter>
                {
                    new SqliteParameter("Name", item.Name),
                    new SqliteParameter("Description", item.Description),
                    new SqliteParameter("Date", item.CreationDate),
                    new SqliteParameter("IsComplete", item.IsComplete)
                };

                var command = new SqliteCommand()
                {
                    CommandText = $@"INSERT INTO ToDoList (Name, Description, CreationDate, IsComplete )
                                    values (@Name, @Description, @Date, @IsComplete);

                                    SELECT last_insert_rowid();",
                    Connection = db
                };

                command.Parameters.AddRange(param);

                var result = 0L;

                result = (long)(await command.ExecuteScalarAsync() ?? 0);

                return (int)result;
            }
        }

        public async Task RecreateTask(TodoItem item)
        {
            using (var db = dbProvider.OpenConnection())
            {
                var param = new List<SqliteParameter>
                    {
                        new SqliteParameter("Name", item.Name),
                        new SqliteParameter("Description", item.Description),
                        new SqliteParameter("CreationDate", item.CreationDate),
                        new SqliteParameter("IsComplete", item.IsComplete),
                        new SqliteParameter("Id", item.Id)
                    };

                var command = new SqliteCommand()
                {
                    CommandText = $@"update ToDoList set 
                                        Name = @Name, 
                                        Description = @Description,
                                        CreationDate = @CreationDate,
                                        IsComplete = @IsComplete
                                        where Id = @Id",
                    Connection = db
                };

                command.Parameters.AddRange(param);

                await command.ExecuteNonQueryAsync();
            }
        }
        

        public async Task DeleteTask(int id)
        {
            using (var db = dbProvider.OpenConnection())
            {
                var param = new List<SqliteParameter>
                    {
                        new SqliteParameter("Id", id)
                    };

                var command = new SqliteCommand()
                {
                    CommandText = $@"DELETE  FROM  ToDoList where id = @Id",
                    Connection = db
                };

                command.Parameters.AddRange(param);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<TodoItem> GetTask(int id)
        {
            using (var db = dbProvider.OpenConnection())
            {
                var param = new List<SqliteParameter>
                    {
                        new SqliteParameter("Id", id)
                    };

                var command = new SqliteCommand()
                {
                    CommandText = $@"SELECT * from ToDoList tdl where Id = @Id",
                    Connection = db
                };

                command.Parameters.AddRange(param);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var result = new TodoItem()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            CreationDate = reader.GetDateTime(3),
                            IsComplete = reader.GetBoolean(4),

                        };

                        return result;
                    }

                    throw new Exception($"Zero tasks found by id {id}");
                }
            }
        }

        public async Task UpdateTask(TodoItem item)
        {
            using (var db = dbProvider.OpenConnection())
            {
                var taskId = item.Id;

                foreach (var property in item.GetType().GetProperties())
                {
                    if (property.Name == "CreationDate" || property.Name == "Id") continue;

                    var param = new List<SqliteParameter>
                    {
                        new SqliteParameter("Value",  property.GetValue(item)),
                        new SqliteParameter("Id", taskId)
                    };

                    var command = new SqliteCommand()
                    {
                        CommandText = $@"update ToDoList set {property.Name} = @Value where Id = @Id and {property.Name} <> @Value",
                        Connection = db
                    };

                    command.Parameters.AddRange(param);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<int> AddRequestRecord(HttpContext context, int taskId = 0)
        {
            using (var db = dbProvider.OpenConnection())
            {

                var param = new List<SqliteParameter>
                {
                    new SqliteParameter("@TaskId", taskId == 0 ? DBNull.Value : taskId),
                    new SqliteParameter("@IP", context.Connection.RemoteIpAddress.ToString()),
                    new SqliteParameter("@RequestTime", DateTime.Now),
                    new SqliteParameter("@RequestPath", context.Request.Path.ToString())
                };

                var command = new SqliteCommand()
                {
                    CommandText = $@"INSERT INTO RequestsAnalitycs 
                                    (
                                        TaskId, IP, RequestTime, RequestPath
                                    )
                                    values 
                                    (
                                        @TaskId, @IP, @RequestTime, @RequestPath
                                    );
                                    SELECT last_insert_rowid();",
                    Connection = db
                };

                command.Parameters.AddRange(param);

                var result = 0L;

                result = (long)(await command.ExecuteScalarAsync() ?? 0);

                return (int)result;
            }
        }

        public async Task UpdateRequestRecord(int taskId, int recordId)
        {
            using (var db = dbProvider.OpenConnection())
            {
                var param = new List<SqliteParameter>
                {
                    new SqliteParameter("TaskId", taskId),
                    new SqliteParameter("RecordId", recordId),
                };

                var command = new SqliteCommand()
                {
                    CommandText = $@"update RequestsAnalitycs set TaskId = @TaskId where id = @RecordId",
                    Connection = db
                };

                command.Parameters.AddRange(param);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<TodoItem>> GetTasks(int page)
        {
            using (var db = dbProvider.OpenConnection())
            {
                var result = new List<TodoItem>();
                var pageLimit = 50;
                var param = new List<SqliteParameter>
                {
                    new SqliteParameter("Skip", pageLimit * page),
                    new SqliteParameter("PageLimit", pageLimit)
                };

                var command = new SqliteCommand()
                {
                    CommandText = $@"SELECT * from ToDoList tdl
                                     Limit @Skip, @PageLimit",
                    Connection = db
                };

                command.Parameters.AddRange(param);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(new TodoItem()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            CreationDate = reader.GetDateTime(3),
                            IsComplete = reader.GetBoolean(4),
                        });
                    }

                    return result;
                }
            }
        }
    }
}