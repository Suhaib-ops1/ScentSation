using ScentSation.Server.Admin.DTO;
using ScentSation.Server.Admin.IDataService;
using ScentSation.Server.Models;

namespace ScentSation.Server.Admin.DataService
{
    public class DataServicee : IDataServicee
    {

        private readonly MyDbContext _db;
        public DataServicee(MyDbContext db)
        {
            _db = db;
        }

        public List<ContactMessageDTO> GetAllContactMessages()
        {
            return _db.ContactMessages
                .OrderByDescending(x => x.SentAt)
                .Select(x => new ContactMessageDTO
                {
                    MessageId = x.MessageId,
                    FullName = x.FullName,
                    Email = x.Email,
                    Subject = x.Subject,
                    Message = x.Message,
                    SentAt = x.SentAt
                })
                .ToList();
        }

        public List<User> GetUsers()
        {

            return _db.Users.Where(u => u.UserRole == "User").ToList(); ;
        }

        public List<User> GetAllStaff()
        {
            return _db.Users.Where(u => u.UserRole == "Staff").ToList();
        }

        public bool AddStaff(StaffDTO staff)
        {
            if (staff == null) return false;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(staff.Password);

            var myUser = new User
            {
                FullName = staff.FullName,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                ProfileImageUrl = staff.ProfileImageUrl,
                PasswordHash = hashedPassword,
                UserRole = "Staff",
                CreatedAt = DateTime.Now
            };

            _db.Users.Add(myUser);
            _db.SaveChanges();
            return true;
        }

        public List<ConsultationAppointment> GetAllBooking()
        {
            return _db.ConsultationAppointments.ToList();
        }


        public bool ConfirmBooking(int id)
        {
            var booking = _db.ConsultationAppointments.FirstOrDefault(b => b.AppointmentId == id);
            if (booking == null) return false;

            booking.Status = "Booked";
            _db.SaveChanges();
            return true;
        }

    }
}
