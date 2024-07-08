using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacinaFacil.Entity.DTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Scheduled { get; set; }
        public DateTime CriationDate { get; set; }
    }
}
