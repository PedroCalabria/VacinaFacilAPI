using VacinaFacil.Entity.Enum;

namespace VacinaFacil.Entity.Entities
{
    public class Appointment : IdEntity<int>
    {
        public int IdPatient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public ScheduledEnum Scheduled { get; set; }
        public DateTime CreationDate { get; set; }

        public Patient Patient { get; set; }

        public Appointment() { }
    }
}
