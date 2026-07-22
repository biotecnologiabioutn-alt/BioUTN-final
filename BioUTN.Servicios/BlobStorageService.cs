using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BioUTN.Servicios
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _connectionString;

        public BlobStorageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureBlobStorage") ?? 
                                throw new InvalidOperationException("AzureBlobStorage connection string not found.");
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string contentType, string containerName, string fileName)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("El stream del archivo es inválido o está vacío.", nameof(fileStream));

            // Crear el cliente del servicio de blobs
            var blobServiceClient = new BlobServiceClient(_connectionString);

            // Obtener referencia al contenedor (lo crea si no existe)
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // Obtener referencia al blob
            var blobClient = containerClient.GetBlobClient(fileName);

            // Configurar los HTTP headers según el tipo de archivo (importante para que se visualicen en el navegador y no se descarguen siempre)
            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            };

            // Subir el archivo
            await blobClient.UploadAsync(fileStream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteFileAsync(string blobUrl, string containerName)
        {
            if (string.IsNullOrEmpty(blobUrl))
                return false;

            try
            {
                // Extraer el nombre del blob desde la URL
                Uri uri = new Uri(blobUrl);
                string blobName = uri.Segments[^1]; // Obtiene el último segmento que es el nombre del archivo

                var blobServiceClient = new BlobServiceClient(_connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(blobName);

                var response = await blobClient.DeleteIfExistsAsync();
                return response.Value;
            }
            catch
            {
                return false;
            }
        }
    }
}
