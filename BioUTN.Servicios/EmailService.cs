using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace BioUTN.Servicios
{
    public class EmailService : IEmailService
    {
        public async Task EnviarCorreoRegistro(string emailDestino, string nombreUsuario)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Plataforma BIO UTN", "biotecnologiabioutn@gmail.com"));
            mensaje.To.Add(new MailboxAddress(nombreUsuario, emailDestino));
            mensaje.Subject = "¡Bienvenido a la Plataforma BIO UTN!";

            mensaje.Body = new TextPart("html")
            {
                Text = $@"
                <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                    <h1 style='color: #275432;'>Hola {nombreUsuario},</h1>
                    <p>Tu registro en la <strong>Plataforma BIO UTN</strong> se ha completado con éxito.</p>
                    <p>Ya puedes acceder al sistema con las credenciales proporcionadas por tu coordinador.</p>
                    <br>
                    <hr style='border: none; border-top: 1px solid #eee;' />
                    <p style='font-size: 0.9em; color: #666;'>Saludos cordiales,<br>El equipo del Laboratorio de Cultivos In Vitro BIO UTN</p>
                </div>"
            };

            using (var cliente = new SmtpClient())
            {
                await cliente.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await cliente.AuthenticateAsync("biotecnologiabioutn@gmail.com", "Bioutn2209tecnologia.");
                await cliente.SendAsync(mensaje);
                await cliente.DisconnectAsync(true);
            }
        }

        public async Task EnviarCorreoRecuperacion(string emailDestino, string urlRecuperacion)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Plataforma BIO UTN", "biotecnologiabioutn@gmail.com"));
            mensaje.To.Add(new MailboxAddress(emailDestino, emailDestino));
            mensaje.Subject = "Recuperación de Contraseña - BIO UTN";

            mensaje.Body = new TextPart("html")
            {
                Text = $@"
                <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                    <h1 style='color: #275432;'>Recuperación de Contraseña</h1>
                    <p>Has solicitado restablecer tu contraseña en la <strong>Plataforma BIO UTN</strong>.</p>
                    <p>Haz clic en el siguiente enlace para crear una nueva contraseña. Este enlace es válido por <strong>5 minutos</strong>.</p>
                    <div style='text-align: center; margin: 30px 0;'>
                        <a href='{urlRecuperacion}' style='background-color: #10b981; color: white; padding: 12px 25px; text-decoration: none; border-radius: 5px; font-weight: bold;'>Restablecer Contraseña</a>
                    </div>
                    <p>Si no solicitaste este cambio, puedes ignorar este correo de forma segura.</p>
                    <br>
                    <hr style='border: none; border-top: 1px solid #eee;' />
                    <p style='font-size: 0.9em; color: #666;'>Saludos cordiales,<br>El equipo del Laboratorio de Cultivos In Vitro BIO UTN</p>
                </div>"
            };

            using (var cliente = new SmtpClient())
            {
                await cliente.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await cliente.AuthenticateAsync("biotecnologiabioutn@gmail.com", "Bioutn2209tecnologia.");
                await cliente.SendAsync(mensaje);
                await cliente.DisconnectAsync(true);
            }
        }
    }
}
