using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.Config 
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddDataDependencyInjectionRepository(this IServiceCollection services)
        {
            services.AddScoped<ICommandeRepository, CommandeRepository>();
            services.AddScoped<ILigneCommandeRepository, LigneCommandeRepository>();
            services.AddScoped<IProduitRepository, ProduitRepository>();
            services.AddScoped<IProprieteRepository, ProprieteRepository>();
            services.AddScoped<ITypeProprieteRepository, TypeProprieteRepository>();

            return services;
        }

    }
}
