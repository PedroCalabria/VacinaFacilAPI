﻿
using Microsoft.EntityFrameworkCore;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;

namespace VacinaFacil.Repository.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(Context context) : base(context) { }

        public Task<List<GroupedAppointmentDTO>> ListAll()
        {
            var entity = EntitySet;
            var query = entity
                .GroupBy(a => new { a.AppointmentDate, a.AppointmentTime })
                .Select(g => new GroupedAppointmentDTO
                {
                    AppointmentDate = g.Key.AppointmentDate,
                    AppointmentTime = g.Key.AppointmentTime,
                    Appointments = g.Select(a => new AppointmentDTO
                    {
                        Id = a.Id,
                        IdPatient = a.IdPatient,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentTime = a.AppointmentTime,
                        Scheduled = a.Scheduled,
                        CreationDate = a.CreationDate,
                    }).ToList(),
                    Count = g.Count()
                });

            return query.ToListAsync();
        }

        public Task<List<GroupedAppointmentDTO>> ListByDate(DateTime date)
        {
            var entity = EntitySet;
            var query = entity
                .Where(a => a.AppointmentDate == date)
                .GroupBy(a => a.AppointmentTime)
                .Select(g => new GroupedAppointmentDTO
                {
                    AppointmentDate = date,
                    AppointmentTime = g.Key,
                    Appointments = g.Select(a => new AppointmentDTO
                                    {
                                        Id = a.Id,
                                        IdPatient = a.IdPatient,
                                        AppointmentDate = a.AppointmentDate,
                                        AppointmentTime = a.AppointmentTime,
                                        Scheduled = a.Scheduled,
                                        CreationDate = a.CreationDate,
                                    }).ToList(),
                    Count = g.Count()
                });

            return query.ToListAsync();
        }

        public Task<Appointment> InsertAppointment(InsertAppointmentModel appointment)
        {
            var newAppointment = new Appointment
            {
                IdPatient = appointment.IdPatient,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Scheduled = appointment.Scheduled,
                CreationDate = DateTime.Now
            };

            return Insert(newAppointment);
        }

        public DbSet<Appointment> GetEntitySet() { return EntitySet; }
    }
}
