using System;
using Azure.Storage.Blobs;
class Program {
    static void Main() {
        try {
            var client = new BlobServiceClient(""DefaultEndpointsProtocol=https;AccountName=bioutnstorage;AccountKey=TU_CLAVE_AQUI;EndpointSuffix=core.windows.net"");
            Console.WriteLine(""Client created"");
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }
}
