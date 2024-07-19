using FluentValidation.AspNetCore;
using VacinaFacil.Validator.Fluent;

namespace VacinaFacil.Api.Configuration
{
    public static class FluentConfiguration
    {
        public static void AddFluentConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<InsertAppointmentValidator>());
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<UpdateAppointmentValidator>());
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<InsertPatientValidator>());
        }
    }
}
