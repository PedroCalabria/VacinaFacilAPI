
using Microsoft.EntityFrameworkCore;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Utils.Group;

namespace VacinaFacil.Repository.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(Context context) : base(context) { }

        public Task<List<AppointmentDTO>> ListAll()
        {
            var entity = EntitySet;
            var query = entity
                .OrderBy(e => e.AppointmentDate)
                .ThenBy(e => e.AppointmentTime)
                .Select(appointment => new AppointmentDTO
                {
                    Id = appointment.Id,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Scheduled = appointment.Scheduled,
                    CriationDate = appointment.CriationDate
                });

            return query.ToListAsync();
        }

        public Task<List<Appointment>> ConsultAppointments(DateTime date, TimeSpan time)
        {
            var query = EntitySet.Where(e => e.AppointmentDate == date && e.AppointmentTime == time);

            return query.ToListAsync();
        }

        public Task<Appointment> InsertAppointment(InsertAppointmentModel appointment)
        {
            var newAppointment = new Appointment
            {
                Id = GetNextId(),
                IdPatient = appointment.IdPatient,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Scheduled = appointment.Scheduled,
                CriationDate = DateTime.Now
            };

            return Insert(newAppointment);
        }

        private int GetNextId()
        {
            var maxId = EntitySet.Max(e => (int?)e.Id) ?? 0;
            return maxId + 1;
        }
    }
}
