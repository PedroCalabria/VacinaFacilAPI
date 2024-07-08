using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Model;

namespace VacinaFacil.Business.Interface.IBusinesses
{
    public interface IAppointmentBusiness
    {
        Task<List<AppointmentDTO>> DeleteAppointment(int idAppointment);
        Task<List<AppointmentDTO>> InsertAppointment(InsertAppointmentModel appointment);
        Task<List<AppointmentDTO>> ListAppointments();
        Task<List<AppointmentDTO>> UpdateAppointment(int idAppointment, UpdateAppointmentModel newAppointment);
    }
}
