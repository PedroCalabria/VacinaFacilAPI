using Microsoft.EntityFrameworkCore;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;

namespace VacinaFacil.Repository.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(Context context) : base(context) { }

        public Task<Patient> InsertPatient(PatientModel patient)
        {
            var newPatient = new Patient
            {
                Name = patient.Name,
                BirthDate = patient.BirthDate,
                CriationDate = DateTime.Now
            };

            return Insert(newPatient);
        }

        public Task<List<PatientDTO>> ListAll()
        {
            var entity = EntitySet;
            var query = entity
                .OrderBy(e => e.Name)
                .Select(patient => new PatientDTO
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    BirthDate = patient.BirthDate,
                    CriationDate = patient.CriationDate
                });

            return query.ToListAsync();
        }
    }
}
