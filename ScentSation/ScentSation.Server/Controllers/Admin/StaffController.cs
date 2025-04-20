using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScentSation.Server.Admin.DTO;
using ScentSation.Server.Admin.IDataService;
using ScentSation.Server.Models;

namespace ScentSation.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IDataServicee _dataServices;

        public StaffController(IDataServicee dataServices)
        {
            _dataServices = dataServices;
        }

        [HttpGet("GetAllStaff")]
        public ActionResult<List<User>> GetAllStaff()
        {
            var staffList = _dataServices.GetAllStaff();
            return Ok(staffList);
        }

        [HttpPost("AddStaff")]
        public ActionResult AddStaff([FromBody] StaffDTO staffDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _dataServices.AddStaff(staffDto);
            if (!result)
                return BadRequest("An error occurred while adding the staff member.");

            return Ok();
        }
    }
}
