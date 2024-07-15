using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Model;

namespace VacinaFacil.Business.Interface.IBusinesses
{
    public interface IPatientBusiness
    {
        Task<List<PatientDTO>> DeletePatient(int idPatient);
        Task<List<PatientDTO>> InsertPatient(InsertPatientModel newPatient);
        Task<List<PatientDTO>> ListPatients();
        Task<List<PatientDTO>> UpdatePatient(int idPatient, UpdatePatientModel newPatient);
    }
}
