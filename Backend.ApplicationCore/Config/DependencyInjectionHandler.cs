using Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Config
{
    public static class DependencyInjectionHandler
    {
        public static IServiceCollection AddMediaRHandlerDependency(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(GetTypeProprieteListHandler).Assembly);
            services.AddMediatR(typeof(AddTypeProprieteHandler).Assembly);
            services.AddMediatR(typeof(RemoveTypeProprieteHandler).Assembly);

            services.AddMediatR(typeof(GetProprieteListHandler).Assembly);
            services.AddMediatR(typeof(AddProprieteHandler).Assembly);
            services.AddMediatR(typeof(RemoveProprieteHandler).Assembly);

            services.AddMediatR(typeof(GetProduitListHandler).Assembly);
            services.AddMediatR(typeof(AddProduitHandler).Assembly);
            services.AddMediatR(typeof(RemoveProduitHandler).Assembly);

            services.AddMediatR(typeof(GetCommandeListHandler).Assembly);
            services.AddMediatR(typeof(AddCommandeHandler).Assembly);
            services.AddMediatR(typeof(RemoveCommandeHandler).Assembly);

            services.AddMediatR(typeof(GetLigneCommandeListHandler).Assembly);
            services.AddMediatR(typeof(AddLigneCommandeHandler).Assembly);
            services.AddMediatR(typeof(RemoveLigneCommandeHandler).Assembly);

            return services;
        }

    }
}
