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
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<List<GroupedAppointmentDTO>> ListAll();
        Task<List<GroupedAppointmentDTO>> ListByDate(DateTime date);
        Task<Appointment> InsertAppointment(InsertAppointmentModel appointment);
    }
}
