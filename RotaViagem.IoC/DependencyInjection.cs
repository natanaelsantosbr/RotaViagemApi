using Microsoft.Extensions.DependencyInjection;
using RotaViagem.Application;
using RotaViagem.Domain;
using RotaViagem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRotaViagemDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IRotaRepository, InMemoryRotaRepository>();
            services.AddScoped<RotaService>();

            return services;
        }
    }
}
