using System.Collections.Generic;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public List<Paciente> ObtenerTodos() => _repository.ObtenerTodos();
        public Paciente? ObtenerPorId(int id) => _repository.ObtenerPorId(id);

    }
}