using VacinaFacil.Api.Middleware;
using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Repository;
using VacinaFacil.Repository.Interface;
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
            InjectMiddlewares(services);

            services.AddScoped<ITransactionManager, TransactionManager>();
        }

        private static void InjectMiddlewares(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
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
