using ScentSation.Server.Models;

namespace ScentSation.Server.firasServices.IDataService
{
    public interface IMyDataService
    {
        public IEnumerable<AvailableSession> getAllAvailableSessions();
        public IEnumerable<ConsultationAppointment> getAllBookedSessions();
        public IEnumerable<ConsultationAppointment> getMyAppointmentsById(int id);
        public bool MyAppointmentsReserve(int id, AvailableSession session);
        public IEnumerable<ConsultationAppointment> getAllStaffSessions(int id);


    }
}
