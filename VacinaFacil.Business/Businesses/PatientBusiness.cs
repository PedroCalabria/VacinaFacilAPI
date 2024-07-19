using log4net;
using System.Security.Cryptography;
using System.Text;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Utils.Exceptions;
using VacinaFacil.Utils.Messages;

namespace VacinaFacil.Business.Businesses
{
    public class PatientBusiness : IPatientBusiness
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PatientBusiness));
        private readonly IPatientRepository _patientRepository;

        public PatientBusiness(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientDTO>> DeletePatient(int idPatient)
        {
            var patient = await _patientRepository.getByID(idPatient);

            if (patient == null)
            {
                _log.InfoFormat(BusinessMessages.RecordNotFound);
                throw new BusinessException(BusinessMessages.RecordNotFound);
            }

            await _patientRepository.Delete(patient);

            _log.InfoFormat(BusinessMessages.SuccessfulOperation);
            return await _patientRepository.ListAll();
        }

        public async Task<List<PatientDTO>> InsertPatient(InsertPatientModel newPatient)
        {
            var patient = await _patientRepository.getPatient(newPatient.Email);

            if (patient != null)
            {
                _log.InfoFormat(string.Format(BusinessMessages.ExistingRecord, patient.Email));
                throw new BusinessException(string.Format(BusinessMessages.ExistingRecord, patient.Email));
            }

            patient = CreatePatient(newPatient);

            await _patientRepository.Insert(patient);

            _log.InfoFormat(BusinessMessages.SuccessfulOperation);
            return await _patientRepository.ListAll();
        }

        private static Patient CreatePatient(InsertPatientModel newPatient)
        {
            var patient = new Patient
            {
                Name = newPatient.Name,
                BirthDate = newPatient.BirthDate,
                Email = newPatient.Email,
                CreationDate = DateTime.Now
            };

            using var hmac = new HMACSHA512();
            patient.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPatient.Password));
            patient.PasswordSalt = hmac.Key;

            return patient;
        }

        public async Task<List<PatientDTO>> ListPatients()
        {
            return await _patientRepository.ListAll();
        }

        public async Task<List<PatientDTO>> UpdatePatient(int idPatient, UpdatePatientModel newPatient)
        {
            var patient = await _patientRepository.getByID(idPatient);

            if (patient == null)
            {
                _log.InfoFormat(BusinessMessages.RecordNotFound);
                throw new BusinessException(BusinessMessages.RecordNotFound);
            }

            patient.Name = newPatient.Name;
            patient.BirthDate = newPatient.BirthDate;
            patient.CreationDate = DateTime.Now;

            await _patientRepository.Update(patient);

            _log.InfoFormat(BusinessMessages.SuccessfulOperation);
            return await _patientRepository.ListAll();
        }
    }
}
