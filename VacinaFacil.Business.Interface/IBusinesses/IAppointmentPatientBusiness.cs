using VacinaFacil.Entity.DTO;

namespace VacinaFacil.Business.Interface.IBusinesses
{
    public interface IAppointmentPatientBusiness
    {
        Task<List<GroupedAppointmentPatientDTO>> ListAppointments();
        Task<List<GroupedAppointmentPatientDTO>> ListAppointmentsByDate(DateTime date);
    }
}
