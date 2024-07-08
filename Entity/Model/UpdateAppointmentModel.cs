using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacinaFacil.Entity.Model
{
    public class UpdateAppointmentModel
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Scheduled { get; set; }
    }
}
