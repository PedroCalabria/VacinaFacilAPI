using Microsoft.AspNetCore.Mvc;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;
using VacinaFacil.Utils.Attributes;

namespace VacinaFacil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        
        [HttpGet("GetListPatients")]
        public async Task<List<PatientDTO>> GetListAppointments()
        {
            return await _patientRepository.ListAll();
        }

        [HttpPost("InsertPatient")]
        [MandatoryTransaction]
        public async Task<Patient> InsertAppointment(PatientModel patient)
        {
            return await _patientRepository.InsertPatient(patient);
        }

    }
}
