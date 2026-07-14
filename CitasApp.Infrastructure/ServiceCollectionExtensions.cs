using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CitasApp.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoriosCitasApp(
            this IServiceCollection services, string entorno, IWebHostEnvironment env)
        {
            services.AddSingleton<IPacienteRepository>(sp =>
            {
                var repo = RepositoryFactory.CrearPacienteRepository(entorno, env);
                return new LoggingPacienteRepository(repo);
            });
            services.AddSingleton<IMedicoRepository>(_ => RepositoryFactory.CrearMedicoRepository(entorno, env));
            services.AddSingleton<ICitaRepository>(_ => RepositoryFactory.CrearCitaRepository(entorno, env));
            return services;
        }
    }
}