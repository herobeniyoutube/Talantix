using Microsoft.AspNetCore.Mvc;
using Talantix.ModelsLibrary.Interfaces;

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
        public IActionResult AddNewTask()
        {

            return View();
        }
    }
}
