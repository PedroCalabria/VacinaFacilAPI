using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;

namespace VacinaFacil.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AppointmentPatientController : ControllerBase
    {
        private readonly IAppointmentPatientBusiness _appointmentpatientBusiness;

        public AppointmentPatientController(IAppointmentPatientBusiness appointmentpatientBusiness)
        {
            _appointmentpatientBusiness = appointmentpatientBusiness;
        }

        [HttpGet("GetListAppointmentsPatients")]
        public async Task<List<GroupedAppointmentPatientDTO>> GetListAppointmentsPatients()
        {
            return await _appointmentpatientBusiness.ListAppointments();
        }

        [HttpGet("GetListAppointmentsPatientsByDate")]
        public async Task<List<GroupedAppointmentPatientDTO>> GetListAppointmentsPatientsByDate(DateTime date)
        {
            return await _appointmentpatientBusiness.ListAppointmentsByDate(date);
        }
    }
}
