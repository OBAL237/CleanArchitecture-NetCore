using Backend.ApplicationCore.Interfaces.IServices;
using Backend.ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.ApplicationCore.Config
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddDependencyInjectionService(this IServiceCollection services)
        {
            services.AddScoped<ITypeProprieteService, TypeProprieteService>();
            services.AddScoped<IProprieteService, ProprieteService>();
            services.AddScoped<IProduitService, ProduitService>();
            services.AddScoped<ICommandeService, CommandeService>();
            services.AddScoped<ILigneCommandeService, LigneCommandeService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

    }
}
