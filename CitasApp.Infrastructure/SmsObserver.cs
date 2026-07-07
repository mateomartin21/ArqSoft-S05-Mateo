using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Observers
{
    public class SmsObserver : ICitaObserver
    {
        public void OnCitaConfirmada(Cita cita)
        {
            Console.WriteLine($"[SMS] Notificación enviada al paciente {cita.PacienteId} — Cita confirmada para el {cita.Fecha:yyyy-MM-dd} a las {cita.Hora}");
        }
    }
}