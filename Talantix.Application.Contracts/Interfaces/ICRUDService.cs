using Microsoft.AspNetCore.Http;
using Talantix.ModelsLibrary.Models;

namespace Talantix.ModelsLibrary.Interfaces
{
    public interface ICRUDService
    {
        public Task<int> AddNewTask(TodoItem item);
        public Task RecreateTask(TodoItem item);
        public Task DeleteTask(int id);
        public Task<List<TodoItem>> GetTasks(int page);
        public Task<TodoItem> GetTask(int id);
        public Task UpdateTask(TodoItem item);
        public Task<int> AddRequestRecord(HttpContext context, int taskId = 0);
        public Task UpdateRequestRecord(int taskId, int recordId);
    }
}
