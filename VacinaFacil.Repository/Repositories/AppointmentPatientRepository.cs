using Microsoft.EntityFrameworkCore;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Repository.Interface.IRepositories;

namespace VacinaFacil.Repository.Repositories
{
    public class AppointmentPatientRepository : IAppointmentPatientRepository
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentPatientRepository(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }

        public Task<List<GroupedAppointmentPatientDTO>> ListAll()
        {
            var appointmentEntity = _appointmentRepository.GetEntitySet();
            var patientEntity = _patientRepository.GetEntitySet();

            var query = appointmentEntity
                        .Join(patientEntity,
                              appointment => appointment.IdPatient,
                              patient => patient.Id,
                              (appointment, patient) => new { appointment, patient })
                        .GroupBy(ap => new { ap.appointment.AppointmentDate, ap.appointment.AppointmentTime })
                        .Select(g => new GroupedAppointmentPatientDTO
                        {
                            AppointmentDate = g.Key.AppointmentDate,
                            AppointmentTime = g.Key.AppointmentTime,
                            Appointments = g.Select(a => new AppointmentPatientDTO
                            {
                                Id = a.appointment.Id,
                                Name = a.patient.Name,
                                BirthDate = a.patient.BirthDate,
                                Email = a.patient.Email,
                                AppointmentDate = a.appointment.AppointmentDate,
                                AppointmentTime = a.appointment.AppointmentTime,
                                Scheduled = a.appointment.Scheduled,
                                CreationDate = a.appointment.CreationDate,
                            }).ToList(),
                            Count = g.Count()
                        });

            return query.ToListAsync();
        }

        public Task<List<GroupedAppointmentPatientDTO>> ListByDate(DateTime date)
        {
            var appointmentEntity = _appointmentRepository.GetEntitySet();
            var patientEntity = _patientRepository.GetEntitySet();

            var query = appointmentEntity
                        .Where(a => a.AppointmentDate == date)
                        .Join(patientEntity,
                              appointment => appointment.IdPatient,
                              patient => patient.Id,
                              (appointment, patient) => new { appointment, patient })
                        .GroupBy(ap => ap.appointment.AppointmentTime)
                        .Select(g => new GroupedAppointmentPatientDTO
                        {
                            AppointmentDate = date,
                            AppointmentTime = g.Key,
                            Appointments = g.Select(a => new AppointmentPatientDTO
                            {
                                Id = a.appointment.Id,
                                Name = a.patient.Name,
                                BirthDate = a.patient.BirthDate,
                                Email = a.patient.Email,
                                AppointmentDate = a.appointment.AppointmentDate,
                                AppointmentTime = a.appointment.AppointmentTime,
                                Scheduled = a.appointment.Scheduled,
                                CreationDate = a.appointment.CreationDate,
                            }).ToList(),
                            Count = g.Count()
                        });

            return query.ToListAsync();
        }
    }
}