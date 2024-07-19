using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacinaFacil.Entity.DTO
{
    public class GroupedAppointmentPatientDTO
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public List<AppointmentPatientDTO> Appointments { get; set; }
        public int Count { get; set; }
    }
}
