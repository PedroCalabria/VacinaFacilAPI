
using Microsoft.EntityFrameworkCore;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
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
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
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
    }
}
