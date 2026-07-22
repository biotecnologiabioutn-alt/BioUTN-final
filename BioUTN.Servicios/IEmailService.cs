using System.Threading.Tasks;

namespace BioUTN.Servicios
{
    public interface IEmailService
    {
        Task EnviarCorreoRegistro(string emailDestino, string nombreUsuario);
        Task EnviarCorreoRecuperacion(string emailDestino, string urlRecuperacion);
    }
}
