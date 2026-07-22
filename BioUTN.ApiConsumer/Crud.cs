using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BioUTN.ApiConsumer
{
    public static class Crud<T>
    {
        public static string EndPoint { get; set; } = string.Empty;

        public static T[] GetAll()
        {
            using var client = new HttpClient();
            var response = client.GetAsync(EndPoint).Result;
            if (!response.IsSuccessStatusCode) return Array.Empty<T>();
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<T[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? Array.Empty<T>();
        }

        public static T? GetById(int id)
        {
            using var client = new HttpClient();
            var response = client.GetAsync($"{EndPoint}/{id}").Result;
            if (!response.IsSuccessStatusCode) return default;
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static bool Create(T data)
        {
            using var client = new HttpClient();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync(EndPoint, content).Result;
            return response.IsSuccessStatusCode;
        }

        public static bool Update(int id, T data)
        {
            using var client = new HttpClient();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PutAsync($"{EndPoint}/{id}", content).Result;
            return response.IsSuccessStatusCode;
        }

        public static bool Delete(int id)
        {
            using var client = new HttpClient();
            var response = client.DeleteAsync($"{EndPoint}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
