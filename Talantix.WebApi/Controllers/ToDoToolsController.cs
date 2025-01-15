using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Talantix.ModelsLibrary.Interfaces;
using Talantix.ModelsLibrary.Models;

namespace Talantix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoToolsController : Controller
    {
        public ToDoToolsController(ICRUDService CRUDService)
        {
            this.CRUDService = CRUDService;
        }

        private readonly ICRUDService CRUDService;

        [HttpPost("AddNewTask")]
        public async Task<IActionResult> AddNewTask(TodoItem item)
        {
            try
            {
                var recordID = await CRUDService.AddRequestRecord(HttpContext);
                var taskID = await CRUDService.AddNewTask(item);
                await CRUDService.UpdateRequestRecord(taskID, recordID);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        [HttpPut("RecreateTask")]
        public async Task<IActionResult> RecreateTask(TodoItem item)
        {
            try
            {
                var recordID = await CRUDService.AddRequestRecord(HttpContext, item.Id);
                await CRUDService.RecreateTask(item);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var recordID = await CRUDService.AddRequestRecord(HttpContext, id);
                await CRUDService.DeleteTask(id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                var recordID = await CRUDService.AddRequestRecord(HttpContext, id);
                return Ok(JsonSerializer.Serialize(await CRUDService.GetTask(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("UpdateTask")]
        public async Task<IActionResult> UpdateTask(TodoItem item)
        {
            try
            {
                var recordID = await CRUDService.AddRequestRecord(HttpContext, item.Id);
                await CRUDService.UpdateTask(item);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        [HttpGet("GetTasks")]
        public async Task<IActionResult> GetTasks(int id)
        {
            try
            {
                var recordID = await CRUDService.AddRequestRecord(HttpContext);
                var result = await CRUDService.GetTasks(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
