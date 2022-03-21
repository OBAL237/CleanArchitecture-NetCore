using Backend.API.Config;
using Backend.ApplicationCore.Config;
using Backend.Infrastructure.Config;
using Backend.Persistence.Config; 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Backend.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration = null;
        private readonly IHostEnvironment _appEnvironment = null;
        public Startup(IConfiguration configuration, IHostEnvironment appEnvironment)
        {
            _configuration = configuration;
            _appEnvironment = appEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();
            services.AddDependencyInjectionService();
            services.AddDataDependencyInjectionRepository();
            services.AddMediaRHandlerDependency();
            services.AddDataDependencyInjectionDbContext();
            services.AddAutoMapperConfig();
            services.AddSwaggerDocumentation();
            AddDatabase(services);
        }



        protected virtual void AddDatabase(IServiceCollection services)
        {
            if (services != null)
            {
                services.AddDatabaseModule(_configuration);
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseApplicationDatabase(serviceProvider);
            app.UseSwaggerDocumentation();
        }

    

    }
}
