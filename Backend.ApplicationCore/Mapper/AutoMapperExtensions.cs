using Backend.ApplicationCore.Mapper;
using Microsoft.Extensions.DependencyInjection;
namespace Backend.API.Config
{
    public static class AutoMapperExtensions 
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }

    }
}
