using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScentSation.Server.firasServices.IDataService;


namespace ScentSation.Server.Controllers.firas
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IMyDataService _data;
        public StaffController(IMyDataService data)
        {
            _data = data;
        }
        [HttpGet("StaffSessions/{id}")]
        public IActionResult GetStaffSessions(int id)
        {

            var staffSessions = _data.getAllStaffSessions(id);
            if (staffSessions == null)
            {
                return NotFound("No Staff Sessions found.");
            }
            else
            {
                return Ok(staffSessions);

            }
        }
    }
}
