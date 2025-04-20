using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScentSation.Server.firasServices.IDataService;
using ScentSation.Server.Models;

namespace ScentSation.Server.Controllers.firas
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMyDataService _data;
        public UserController(IMyDataService data)
        {

            _data = data;
        }
        [HttpGet("AvailableSessions")]
        public ActionResult AvailableSessions()
        {
            var availableSessions = _data.getAllAvailableSessions();

            if (availableSessions == null)
            {
                return NotFound("No Available Sessions found.");
            }
            return Ok(availableSessions);
        }

        [HttpGet("BookedSessions")]
        public ActionResult BookedSessions()
        {
            var availableSessions = _data.getAllBookedSessions();

            if (availableSessions == null)
            {
                return NotFound("No Available Sessions found.");
            }
            return Ok(availableSessions);
        }
        [HttpGet("getMyAppointments/{id}")]
        public ActionResult getMyAppointments(int id)
        {
            var Appointments = _data.getMyAppointmentsById(id);
            if (Appointments == null)
                return NoContent();
            return Ok(Appointments);
        }

        [HttpPut("ReserveSession/{id}")]
        public IActionResult PutMyAppointments(int id, [FromBody] AvailableSession session)
        {
            var isReserved = _data.MyAppointmentsReserve(id, session);

            if (isReserved)
            {
                return Ok("Your appointment has been reserved.");
            }
            else
            {
                return BadRequest("Failed to reserve the appointment.");
            }
        }
    }
}
