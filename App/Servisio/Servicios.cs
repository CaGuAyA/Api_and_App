using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using App.Modelo;

namespace App.Servisio
{
    internal class Servicios
    {
        private readonly HttpClient _httpClient;

        public Servicios()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7294/CoordinatorControllers/") };
        }

        public async Task<List<JoinModelos>> GetAllTables()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<JoinModelos>>(content);
        }

        public async Task<JoinModelos> GetProductAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JoinModelos>(content);
        }

        public async Task<byte[]> GeneratePdfAsync(int id)
        {
            var response = await _httpClient.GetAsync($"GeneratePDF/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        
        public async Task CreateProductAsync(CreateUpdateRequest createUpdateRequest)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(createUpdateRequest), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/your-endpoint", content);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return;
            }
            catch (HttpRequestException ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error en la solicitud: {ex.Message}");
                return; // O maneja el error de otra manera
            }
        }

        /*
        public async Task UpdateProductAsync(int id, CreateUpdateRequest product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _client.DeleteAsync($"{id}");
            response.EnsureSuccessStatusCode();
        }
        */
    }
}
