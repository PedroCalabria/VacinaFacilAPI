using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;

namespace VacinaFacil.Business.Businesses
{
    public class AppointmentBusiness : IAppointmentBusiness
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentBusiness(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<AppointmentDTO>> DeleteAppointment(int idAppointment)
        {
            var appointment = await _appointmentRepository.getByID(idAppointment);

            if (appointment != null)
            {
                await _appointmentRepository.Delete(appointment);
            }
            else
            {
                throw new Exception();
            }

            return await _appointmentRepository.ListAll();

        }

        public async Task<List<AppointmentDTO>> InsertAppointment(InsertAppointmentModel appointment)
        {
            var appointmentAvailability = await CheckAppointmentAvailability(appointment.AppointmentDate, appointment.AppointmentTime);
            
            if (!appointmentAvailability)
            {
                throw new Exception();
            }

            await _appointmentRepository.InsertAppointment(appointment);

            return await _appointmentRepository.ListAll();
        }

        public async Task<List<AppointmentDTO>> ListAppointments()
        {
            return await _appointmentRepository.ListAll();
        }

        public async Task<List<AppointmentDTO>> UpdateAppointment(int idAppointment, UpdateAppointmentModel newAppointment)
        {
            var appointment = await _appointmentRepository.getByID(idAppointment);

            if (appointment == null)
            {
                throw new Exception();
            }

            var appointmentAvailability = await CheckAppointmentAvailability(newAppointment.AppointmentDate, newAppointment.AppointmentTime);

            if (!appointmentAvailability)
            {
                throw new Exception();
            }

            appointment.AppointmentDate = newAppointment.AppointmentDate;
            appointment.AppointmentTime = newAppointment.AppointmentTime;
            appointment.Scheduled = newAppointment.Scheduled;
            appointment.CriationDate = DateTime.Now;

            return await _appointmentRepository.ListAll();
        }

        private async Task<bool> CheckAppointmentAvailability(DateTime date, TimeSpan time)
        {
            var appointments = await _appointmentRepository.ConsultAppointments(date, time);

            if (appointments.Count >= 2)
            {
                return false;
            }

            return true;
        }
    }
}
