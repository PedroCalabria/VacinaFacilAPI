using Microsoft.AspNetCore.Mvc;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Model;

namespace VacinaFacil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBusiness _appointmentBusiness;

        public AppointmentController(IAppointmentBusiness appointmentBusiness)
        {
            _appointmentBusiness = appointmentBusiness;
        }

        [HttpDelete("DeleteAppointment")]
        public async Task<List<AppointmentDTO>> DeleteAppointment(int idAppointment)
        {
            return await _appointmentBusiness.DeleteAppointment(idAppointment);
        }
        
        [HttpGet("GetListAppointments")]
        public async Task<List<AppointmentDTO>> GetListAppointments()
        {
            return await _appointmentBusiness.ListAppointments();
        }

        [HttpPost("InsertAppointment")]
        public async Task<List<AppointmentDTO>> InsertAppointment(InsertAppointmentModel appointment)
        {
            return await _appointmentBusiness.InsertAppointment(appointment);
        }

        [HttpPut("UpdateAppointment")]
        public async Task<List<AppointmentDTO>> UpdateAppointment(int idAppointment, UpdateAppointmentModel newAppointment)
        {
            return await _appointmentBusiness.UpdateAppointment(idAppointment, newAppointment);
        }
    }
}
