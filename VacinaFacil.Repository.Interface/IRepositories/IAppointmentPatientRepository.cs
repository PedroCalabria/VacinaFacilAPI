using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.DTO;

namespace VacinaFacil.Repository.Interface.IRepositories
{
    public interface IAppointmentPatientRepository
    {
        Task<List<GroupedAppointmentPatientDTO>> ListAll();
        Task<List<GroupedAppointmentPatientDTO>> ListByDate(DateTime date);
    }
}
