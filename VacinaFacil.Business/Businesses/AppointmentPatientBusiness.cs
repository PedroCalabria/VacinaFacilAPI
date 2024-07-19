using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Repository.Interface.IRepositories;

namespace VacinaFacil.Business.Businesses
{
    public class AppointmentPatientBusiness: IAppointmentPatientBusiness
    {
        private readonly IAppointmentPatientRepository _appointmentpatientRepository;

        public AppointmentPatientBusiness(IAppointmentPatientRepository appointmentpatientRepository)
        {
            _appointmentpatientRepository = appointmentpatientRepository;
        }

        public async Task<List<GroupedAppointmentPatientDTO>> ListAppointments()
        {
            return await _appointmentpatientRepository.ListAll();
        }
        
        public async Task<List<GroupedAppointmentPatientDTO>> ListAppointmentsByDate(DateTime date)
        {
            return await _appointmentpatientRepository.ListByDate(date);
        }
    }
}
