using ControleTarefas.WebApi.Middleware;
using VacinaFacil.Api.Middleware;
using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Repository;
using VacinaFacil.Repository.Interface;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;
using VacinaFacil.Utils.Configurations;
using VacinaFacil.Utils.PatientContext;

namespace VacinaFacil.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectRepositories(services);
            InjectBusinesses(services);
            InjectMiddlewares(services);

            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IPatientContext, PatientContext>();
            services.AddOptions<AuthenticationConfig>().Bind(configuration.GetSection("Authorization"));
        }

        private static void InjectMiddlewares(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
            services.AddTransient<PatientContextMiddleware>();
        }

        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentPatientRepository, AppointmentPatientRepository>();
        }

        private static void InjectBusinesses(IServiceCollection services)
        {
            services.AddScoped<IAppointmentBusiness, AppointmentBusiness>();
            services.AddScoped<IPatientBusiness, PatientBusiness>();
            services.AddScoped<IAppointmentPatientBusiness, AppointmentPatientBusiness>();
            services.AddScoped<IAuthenticationBusiness, AuthenticationBusiness>();
        }
    }
}
