using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Model;

namespace VacinaFacil.Business.Interface.IBusinesses
{
    public interface IAppointmentBusiness
    {
        Task<List<GroupedAppointmentDTO>> DeleteAppointment(int idAppointment);
        Task<List<GroupedAppointmentDTO>> InsertAppointment(InsertAppointmentModel appointment);
        Task<List<GroupedAppointmentDTO>> ListAppointments();
        Task<List<GroupedAppointmentDTO>> ListAppointmentsByDate(DateTime date);
        Task<List<GroupedAppointmentDTO>> UpdateAppointment(int idAppointment, UpdateAppointmentModel newAppointment);
    }
}
