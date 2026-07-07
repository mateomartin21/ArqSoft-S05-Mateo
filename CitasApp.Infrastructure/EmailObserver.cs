using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Observers
{
    public class EmailObserver : ICitaObserver
    {
        public void OnCitaConfirmada(Cita cita)
        {
            Console.WriteLine($"[Email] Correo enviado al paciente {cita.PacienteId} — Cita confirmada para el {cita.Fecha:yyyy-MM-dd} a las {cita.Hora}");
        }
    }
}