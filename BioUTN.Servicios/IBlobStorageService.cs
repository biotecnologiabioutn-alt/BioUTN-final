using System.IO;
using System.Threading.Tasks;

namespace BioUTN.Servicios
{
    public interface IBlobStorageService
    {
        /// <summary>
        /// Sube un archivo a Azure Blob Storage.
        /// </summary>
        /// <param name="fileStream">Stream del archivo a subir</param>
        /// <param name="contentType">Tipo de contenido MIME (ej. 'application/pdf')</param>
        /// <param name="containerName">Nombre del contenedor (ej. 'biblioteca', 'plantasmadre')</param>
        /// <param name="fileName">Nombre final del archivo</param>
        /// <returns>La URL pública del archivo subido</returns>
        Task<string> UploadFileAsync(Stream fileStream, string contentType, string containerName, string fileName);

        /// <summary>
        /// Elimina un archivo de Azure Blob Storage.
        /// </summary>
        /// <param name="blobUrl">URL pública del archivo</param>
        /// <param name="containerName">Nombre del contenedor</param>
        /// <returns>True si se eliminó, False si no</returns>
        Task<bool> DeleteFileAsync(string blobUrl, string containerName);
    }
}
