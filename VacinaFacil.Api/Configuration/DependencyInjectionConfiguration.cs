using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;

namespace VacinaFacil.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            InjectRepositories(services);
            InjectBusinesses(services);
        }

        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }

        private static void InjectBusinesses(IServiceCollection services)
        {
            services.AddScoped<IAppointmentBusiness, AppointmentBusiness>();
        }
    }
}
