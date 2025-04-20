using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScentSation.Server.Admin.IDataService;

namespace ScentSation.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IDataServicee _data;

        public usersController(IDataServicee dataService)
        {
            _data = dataService;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var serv = _data.GetUsers();
            return Ok(serv);
        }
    }
    }