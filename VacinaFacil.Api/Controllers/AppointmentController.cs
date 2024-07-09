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

        public AppointmentController(IAppointmentBusiness appointmentBusiness)
        {
            _appointmentBusiness = appointmentBusiness;
        }

        [HttpDelete("DeleteAppointment")]
        [MandatoryTransaction]
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
        [MandatoryTransaction]
        public async Task<List<AppointmentDTO>> InsertAppointment(InsertAppointmentModel appointment)
        {
            return await _appointmentBusiness.InsertAppointment(appointment);
        }

        [HttpPut("UpdateAppointment")]
        [MandatoryTransaction]
        public async Task<List<AppointmentDTO>> UpdateAppointment(int idAppointment, UpdateAppointmentModel newAppointment)
        {
            return await _appointmentBusiness.UpdateAppointment(idAppointment, newAppointment);
        }
    }
}
