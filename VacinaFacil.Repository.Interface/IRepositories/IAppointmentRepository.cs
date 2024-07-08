using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;

namespace VacinaFacil.Repository.Interface.IRepositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<List<AppointmentDTO>> ListAll();
    }
}
