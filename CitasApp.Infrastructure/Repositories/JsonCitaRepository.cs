using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonCitaRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "citas.json");
        }
        // -- Helpers privados ------------------------------------------------

        private List<CitaJson> LeerJson()
        {
            if (!File.Exists(_path)) return new();
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<CitaJson>>(json, _options) ?? new();
        }

        private void Guardar(List<CitaJson> lista)
        {
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
        }

        private static Cita MapearACita(CitaJson c) => new()
        {
            Id = c.Id,
            PacienteId = c.PacienteId,
            MedicoId = c.MedicoId,
            Fecha = DateOnly.Parse(c.Fecha),
            Hora = TimeOnly.Parse(c.Hora),
            Motivo = c.Motivo,
            Estado = c.Estado
        };

        // -- Métodos de la interfaz ------------------------------------------

        public List<Cita> ObtenerTodos() =>
            LeerJson().Select(MapearACita).ToList();

        public List<Cita> ObtenerPorPaciente(int pacienteId) =>
            ObtenerTodos().Where(c => c.PacienteId == pacienteId).ToList();

        public void Agregar(Cita cita)
        {
            var lista = LeerJson();
            var nuevoId = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;

            lista.Add(new CitaJson
            {
                Id = nuevoId,
                PacienteId = cita.PacienteId,
                MedicoId = cita.MedicoId,
                Fecha = cita.Fecha.ToString("yyyy-MM-dd"),
                Hora = cita.Hora.ToString("HH:mm"),
                Motivo = cita.Motivo,
                Estado = cita.Estado
            });

            Guardar(lista);
        }

        public void Eliminar(int id)
        {
            var lista = LeerJson();
            lista.RemoveAll(c => c.Id == id);
            Guardar(lista);
        }
    }
}
