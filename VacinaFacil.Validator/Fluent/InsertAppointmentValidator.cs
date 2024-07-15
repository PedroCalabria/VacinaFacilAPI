using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.Enum;
using VacinaFacil.Entity.Model;
using VacinaFacil.Utils.Messages;

namespace VacinaFacil.Validator.Fluent
{
    public class InsertAppointmentValidator : AbstractValidator<InsertAppointmentModel>
    {
        public InsertAppointmentValidator()
        {
            RuleFor(appointment => appointment.IdPatient)
                .NotNull().WithMessage(string.Format(BusinessMessages.MandatoryField, "IdPatiente"))
                .GreaterThan(0).WithMessage(string.Format(BusinessMessages.MinValueField, "IdPatient", 1));

            RuleFor(appointment => appointment.AppointmentDate)
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "Data do agendamento"))
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage(string.Format(BusinessMessages.MinDate, "Data do agendamento", DateTime.Now.Date));

            RuleFor(appointment => appointment.AppointmentTime)
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "Hora do agendamento"));

            RuleFor(appointment => appointment.Scheduled)
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "Agendado"))
                .IsInEnum().WithMessage(string.Format(BusinessMessages.EnumOutOfRange, "Agendado"));
        }
    }
}
