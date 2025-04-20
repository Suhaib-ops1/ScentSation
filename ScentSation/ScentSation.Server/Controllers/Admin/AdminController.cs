using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScentSation.Server.Admin.IDataService;

namespace ScentSation.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IDataServicee _dataService;

        public AdminController(IDataServicee dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("GetAllContactMessages")]
        public IActionResult GetAllContactMessages()
        {
            var messages = _dataService.GetAllContactMessages();

            if (messages == null || !messages.Any())
                return NoContent(); // 204

            return Ok(messages); // 200
        }
    }
}