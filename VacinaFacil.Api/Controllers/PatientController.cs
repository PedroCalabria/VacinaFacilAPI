using Microsoft.AspNetCore.Authorization;
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

        [HttpDelete("DeletePatient")]
        [MandatoryTransaction]
        [Authorize]
        public async Task<List<PatientDTO>> DeletePatient(int idPatient)
        {
            return await _patientBusiness.DeletePatient(idPatient);
        }

        [HttpGet("GetListPatients")]
        public async Task<List<PatientDTO>> GetListPatients()
        {
            return await _patientBusiness.ListPatients();
        }

        [HttpPost("InsertPatient")]
        [MandatoryTransaction]
        public async Task<List<PatientDTO>> InsertPatient(InsertPatientModel patient)
        {
            return await _patientBusiness.InsertPatient(patient);
        }

        [HttpPut("UpdatePatient")]
        [MandatoryTransaction]
        [Authorize]
        public async Task<List<PatientDTO>> UpdatePatient(int idPatient, UpdatePatientModel newPatient)
        {
            return await _patientBusiness.UpdatePatient(idPatient, newPatient);
        }

    }
}
