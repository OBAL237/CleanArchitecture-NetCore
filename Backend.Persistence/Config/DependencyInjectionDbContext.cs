
using Backend.ApplicationCore.Interfaces.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Persistence.Config
{
    public static class DependencyInjectionDbContext
    {
        public static IServiceCollection AddDataDependencyInjectionDbContext(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDatabaseContext, ApplicationDatabaseContext>();

            return services;
        }

    }
}
