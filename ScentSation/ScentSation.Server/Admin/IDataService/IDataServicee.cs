using ScentSation.Server.Admin.DTO;
using ScentSation.Server.Models;

namespace ScentSation.Server.Admin.IDataService
{
    public interface IDataServicee
    {
        public List<ContactMessageDTO> GetAllContactMessages();

        public List<User> GetUsers();

        public List<User> GetAllStaff();

        public bool AddStaff(StaffDTO staff);

        //----------------------------------------------------------

        public List<ConsultationAppointment> GetAllBooking();

        public bool ConfirmBooking(int id);

    }
}
