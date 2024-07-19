using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;

namespace VacinaFacil.Repository.Interface.IRepositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<List<PatientDTO>> ListAll();
        Task<Patient> getPatient(string email);
        DbSet<Patient> GetEntitySet();
    }
}
