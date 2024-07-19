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
                    Email = patient.Email,
                    CreationDate = patient.CreationDate
                });

            return query.ToListAsync();
        }

        public Task<Patient> getPatient(string email)
        {
            return EntitySet.FirstOrDefaultAsync(p => p.Email.ToLower() == email.ToLower());
        }

        public DbSet<Patient> GetEntitySet() { return EntitySet; }
    }
}
