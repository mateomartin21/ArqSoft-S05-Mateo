using CitasApp.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(
            string entorno, IWebHostEnvironment env)
        {
            var dataFolder = Path.Combine(env.ContentRootPath, "data");

            return entorno switch
            {
                "Production" => new SqlitePacienteRepository(dataFolder),
                _ => new JsonPacienteRepository(dataFolder)
            };
        }

        public static IMedicoRepository CrearMedicoRepository(
            string entorno, IWebHostEnvironment env)
        {
            var dataFolder = Path.Combine(env.ContentRootPath, "data");

            return entorno switch
            {
                "Production" => new SqliteMedicoRepository(dataFolder),
                _ => new JsonMedicoRepository(dataFolder)
            };
        }

        public static ICitaRepository CrearCitaRepository(
            string entorno, IWebHostEnvironment env)
        {
            var dataFolder = Path.Combine(env.ContentRootPath, "data");

            return entorno switch
            {
                "Production" => new SqliteCitaRepository(dataFolder),
                _ => new JsonCitaRepository(dataFolder)
            };
        }
    }
}