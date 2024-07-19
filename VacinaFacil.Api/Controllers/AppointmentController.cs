using Microsoft.AspNetCore.Mvc;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Model;
using VacinaFacil.Utils.Attributes;

namespace VacinaFacil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBusiness _appointmentBusiness;
        private readonly IAppointmentPatientBusiness _appointmentpatientBusiness;

        public AppointmentController(IAppointmentBusiness appointmentBusiness, IAppointmentPatientBusiness appointmentpatientBusiness)
        {
            _appointmentBusiness = appointmentBusiness;
            _appointmentpatientBusiness = appointmentpatientBusiness;
        }

        [HttpDelete("DeleteAppointment")]
        [MandatoryTransaction]
        public async Task<List<GroupedAppointmentDTO>> DeleteAppointment(int idAppointment)
        {
            return await _appointmentBusiness.DeleteAppointment(idAppointment);
        }
        
        [HttpGet("GetListAppointments")]
        public async Task<List<GroupedAppointmentDTO>> GetListAppointments()
        {
            return await _appointmentBusiness.ListAppointments();
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

        [HttpGet("GetListAppointmentsByDate")]
        public async Task<List<GroupedAppointmentDTO>> GetListAppointmentsByDate(DateTime date)
        {
            return await _appointmentBusiness.ListAppointmentsByDate(date);
        }

        [HttpPost("InsertAppointment")]
        [MandatoryTransaction]
        public async Task<List<GroupedAppointmentDTO>> InsertAppointment(InsertAppointmentModel appointment)
        {
            return await _appointmentBusiness.InsertAppointment(appointment);
        }

        [HttpPut("UpdateAppointment")]
        [MandatoryTransaction]
        public async Task<List<GroupedAppointmentDTO>> UpdateAppointment(UpdateAppointmentModelFull newAppointment)
        {
            return await _appointmentBusiness.UpdateAppointment(newAppointment.IdAppointment, newAppointment.newAppointment);
        }
    }
}
