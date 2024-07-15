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
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientModel>
    {
        public UpdatePatientValidator() 
        {
            RuleFor(patient => patient.Name)
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "Nome"))
                .MaximumLength(50).WithMessage(string.Format(BusinessMessages.MaxSizeField, "Name", 50));

            RuleFor(patient => patient.BirthDate)
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "Data de nascimento"))
                .LessThan(DateTime.Now).WithMessage(string.Format(BusinessMessages.MaxDate, "Data de nascimento", DateTime.Now.Date))
                .GreaterThan(DateTime.Now.Date.AddYears(-120)).WithMessage(string.Format(BusinessMessages.MinDate, "Data de nascimento", DateTime.Now.Date.AddYears(-120)));

            RuleFor(patient => patient.Email)
                .EmailAddress().WithMessage(string.Format(BusinessMessages.InvalidField, "Email"))
                .NotEmpty().WithMessage(string.Format(BusinessMessages.MandatoryField, "Email"));
        }
    }
}
