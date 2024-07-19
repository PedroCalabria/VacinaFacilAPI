using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.Enum;

namespace VacinaFacil.Entity.Model
{
    public class UpdateAppointmentModelFull
    {
        public int IdAppointment { get; set; }
        public UpdateAppointmentModel newAppointment { get; set; }
    }
}
