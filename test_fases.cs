using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program {
    static async Task Main() {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:8080/api/FaseCultivos");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
