using CitasApp.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        private static string ObtenerDataFolder(IWebHostEnvironment env) =>
            Path.Combine(env.ContentRootPath, "data");

        public static IPacienteRepository CrearPacienteRepository(string entorno, IWebHostEnvironment env)
        {
            var dataFolder = ObtenerDataFolder(env);
            return entorno == "Production"
                ? new SqlitePacienteRepository(dataFolder)
                : new JsonPacienteRepository(dataFolder);
        }

        public static IMedicoRepository CrearMedicoRepository(string entorno, IWebHostEnvironment env)
        {
            var dataFolder = ObtenerDataFolder(env);
            return entorno == "Production"
                ? new SqliteMedicoRepository(dataFolder)
                : new JsonMedicoRepository(dataFolder);
        }

        public static ICitaRepository CrearCitaRepository(string entorno, IWebHostEnvironment env)
        {
            var dataFolder = ObtenerDataFolder(env);
            return entorno == "Production"
                ? new SqliteCitaRepository(dataFolder)
                : new JsonCitaRepository(dataFolder);
        }
    }
}