using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;

namespace VacinaFacil.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            InjetarRepositorio(services);
        }

        private static void InjetarRepositorio(IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }
    }
}
