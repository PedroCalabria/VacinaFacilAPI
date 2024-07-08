using Microsoft.AspNetCore.Mvc;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;

namespace VacinaFacil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet("GetListAppointments")]
        public async Task<List<AppointmentDTO>> GetListAppointments()
        {
            return await _appointmentRepository.ListAll();
        }
    }
}
