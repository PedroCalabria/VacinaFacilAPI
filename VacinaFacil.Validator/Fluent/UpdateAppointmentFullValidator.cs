using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.Model;
using VacinaFacil.Utils.Messages;

namespace VacinaFacil.Validator.Fluent
{
    public class UpdateAppointmentFullValidator : AbstractValidator<UpdateAppointmentModelFull>
    {
        public UpdateAppointmentFullValidator() {

            RuleFor(appointment => appointment.IdAppointment)
                    .NotNull().WithMessage(string.Format(BusinessMessages.MandatoryField, "IdAppointment"))
                    .GreaterThan(0).WithMessage(string.Format(BusinessMessages.MinValueField, "IdAppointment", 1));

            RuleFor(x => x.newAppointment)
                    .NotNull().WithMessage("The new appointment details must not be null.")
                    .SetValidator(new UpdateAppointmentValidator());
        }
    }
}
