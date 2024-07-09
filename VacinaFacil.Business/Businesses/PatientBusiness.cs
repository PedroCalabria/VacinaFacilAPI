using log4net;
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

        public async Task<List<PatientDTO>> InsertPatient(PatientModel newPatient)
        {
            var patient = await _patientRepository.getPatient(newPatient.Name, newPatient.BirthDate);

            if (patient != null)
            {
                _log.InfoFormat(string.Format(BusinessMessages.ExistingRecord, new { newPatient.Name, newPatient.BirthDate }));
                throw new BusinessException(string.Format(BusinessMessages.ExistingRecord, new { newPatient.Name, newPatient.BirthDate }));
            }

            await _patientRepository.InsertPatient(newPatient);

            _log.InfoFormat(BusinessMessages.SuccessfulOperation);
            return await _patientRepository.ListAll();
        }

        public async Task<List<PatientDTO>> ListPatients()
        {
            return await _patientRepository.ListAll();
        }

        public async Task<List<PatientDTO>> UpdatePatient(int idPatient, PatientModel newPatient)
        {
            var patient = await _patientRepository.getByID(idPatient);

            if (patient == null)
            {
                _log.InfoFormat(BusinessMessages.RecordNotFound);
                throw new BusinessException(BusinessMessages.RecordNotFound);
            }

            patient.Name = newPatient.Name;
            patient.BirthDate = newPatient.BirthDate;
            patient.CriationDate = DateTime.Now;

            await _patientRepository.Update(patient);

            _log.InfoFormat(BusinessMessages.SuccessfulOperation);
            return await _patientRepository.ListAll();
        }
    }
}
