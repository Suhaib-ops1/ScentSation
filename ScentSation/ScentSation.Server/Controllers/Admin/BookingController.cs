using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScentSation.Server.Admin.IDataService;
using ScentSation.Server.Models;

namespace ScentSation.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IDataServicee _data;

        public BookingController(IDataServicee data)
        {
            _data = data;
        }

        [HttpGet("GetAllBookings")]
        public ActionResult<List<ConsultationAppointment>> GetAllBookings()
        {
            var bookings = _data.GetAllBooking();
            return Ok(bookings);
        }

        [HttpPut("ConfirmBooking/{id}")]
        public IActionResult ConfirmBooking(int id)
        {
            bool result = _data.ConfirmBooking(id);
            if (!result) return NotFound();

            return Ok();
        }
    }
}
