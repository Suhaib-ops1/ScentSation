using System.Diagnostics.Eventing.Reader;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.firasServices.IDataService;
using ScentSation.Server.Models;

namespace CoreApiAngular.Server.DataService
{
    public class MyDataService : IMyDataService
    {
        private readonly MyDbContext _db;

        public MyDataService(MyDbContext db)
        {
            _db = db;
        }

        public IEnumerable<AvailableSession> getAllAvailableSessions()
        {

            var sessions = _db.AvailableSessions.Where(s => s.Status == "Available").ToList();

            return sessions;

        }

        public IEnumerable<ConsultationAppointment> getAllBookedSessions()

        {

            var bookedsessions = _db.ConsultationAppointments.ToList();

            return bookedsessions;

        }


        public IEnumerable<ConsultationAppointment> getMyAppointmentsById(int id)
        {
            var Apointments = _db.ConsultationAppointments.Where(appointment => appointment.UserId == id).ToList();

            return Apointments;
        }
        public bool MyAppointmentsReserve(int id, AvailableSession session)
        {
            var appointment = _db.AvailableSessions.Find(id);
            if (appointment != null)
            {
                appointment.Status = "Booked";
                _db.Update(appointment);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<ConsultationAppointment> getAllStaffSessions(int id)
        {
            var staffSessions = _db.ConsultationAppointments.Where(s => s.StaffId == id && s.Status == "Booked");
            return staffSessions;
        }
    }
}