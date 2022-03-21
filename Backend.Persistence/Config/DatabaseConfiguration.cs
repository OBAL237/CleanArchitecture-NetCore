using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Persistence.Config;

namespace Backend.Persistence.Config
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection @this, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("AppDbContext");
            @this.AddDbContextPool<ApplicationDatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            return @this;
        }

        public static IApplicationBuilder UseApplicationDatabase(this IApplicationBuilder @this,
            IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDatabaseContext>();
                context.Database.OpenConnection();
                context.Database.Migrate();
            }
            return @this;
        }
    }
}
