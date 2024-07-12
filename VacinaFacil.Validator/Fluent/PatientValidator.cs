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
    public class PatientValidator : AbstractValidator<PatientModel>
    {
        public PatientValidator() 
        {
            RuleFor(patient => patient.Name)
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "Name"))
                .MaximumLength(50).WithMessage(string.Format(BusinessMessages.MaxSizeField, "Name", 50));

            RuleFor(patient => patient.BirthDate)
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "BirthDate"))
                .LessThan(DateTime.Now).WithMessage(string.Format(BusinessMessages.MaxDate, "BirthDate", DateTime.Now.Date))
                .GreaterThan(DateTime.Now.Date.AddYears(-120)).WithMessage(string.Format(BusinessMessages.MinDate, "BirthDate", DateTime.Now.Date.AddYears(-120)));
        }
    }
}
