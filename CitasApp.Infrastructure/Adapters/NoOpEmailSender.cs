using Microsoft.AspNetCore.Identity.UI.Services;

namespace CitasApp.Infrastructure.Adapters
{
    public class NoOpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // No se envía correo real; el registro de usuarios no depende de confirmación por email.
            return Task.CompletedTask;
        }
    }
}