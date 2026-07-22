using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repository;
        private readonly List<ICitaObserver> _observers = new();

        public CitaService(ICitaRepository repository)
        {
            _repository = repository;
        }

        // Permite registrar observers desde Program.cs
        public void AgregarObserver(ICitaObserver observer)
            => _observers.Add(observer);

        public List<Cita> ObtenerTodos() => _repository.ObtenerTodos();

        public List<Cita> ObtenerPorPaciente(int pacienteId)
            => _repository.ObtenerPorPaciente(pacienteId);

        public void Agregar(Cita cita)
        {
            _repository.Agregar(cita);

            // Notifica observers solo si la cita es Confirmada
            if (cita.Estado == "Confirmada")
            {
                foreach (var observer in _observers)
                    observer.OnCitaConfirmada(cita);
            }
        }

        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}