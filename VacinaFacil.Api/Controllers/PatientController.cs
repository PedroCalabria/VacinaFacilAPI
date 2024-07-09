using Microsoft.AspNetCore.Mvc;
using VacinaFacil.Business.Businesses;
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
        private readonly IPatientBusiness _patientBusiness;

        public PatientController(IPatientBusiness patientBusiness)
        {
            _patientBusiness = patientBusiness;
        }

        [HttpDelete("DeleteAppointment")]
        [MandatoryTransaction]
        public async Task<List<PatientDTO>> DeleteAppointment(int idPatient)
        {
            return await _patientBusiness.DeletePatient(idPatient);
        }

        [HttpGet("GetListPatients")]
        public async Task<List<PatientDTO>> GetListAppointments()
        {
            return await _patientBusiness.ListPatients();
        }

        [HttpPost("InsertPatient")]
        [MandatoryTransaction]
        public async Task<List<PatientDTO>> InsertAppointment(PatientModel patient)
        {
            return await _patientBusiness.InsertPatient(patient);
        }

        [HttpPut("UpdateAppointment")]
        [MandatoryTransaction]
        public async Task<List<PatientDTO>> UpdateAppointment(int idPatient, PatientModel newPatient)
        {
            return await _patientBusiness.UpdatePatient(idPatient, newPatient);
        }

    }
}
