using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.Enum;

namespace VacinaFacil.Entity.Model
{
    public class InsertAppointmentModel
    {
        public int IdPatient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public ScheduledEnum Scheduled { get; set; }
    }
}
