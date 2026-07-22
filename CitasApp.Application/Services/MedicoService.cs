using System.Collections.Generic;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Application.Services
{
    public class MedicoService
    {
        private readonly IMedicoRepository _repository;

        public MedicoService(IMedicoRepository repository)
        {
            _repository = repository;
        }

        public List<Medico> ObtenerTodos() => _repository.ObtenerTodos();
        public Medico? ObtenerPorId(int id) => _repository.ObtenerPorId(id);

    }
}