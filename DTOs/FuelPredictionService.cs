using System.Net.Http.Json;
using BUA_project.DTOs;

namespace BUA_project.Services
{
    public class FuelPredictionService
    {
        private readonly HttpClient _httpClient;

        public FuelPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FuelPredictionResponse?> PredictAsync(
            FuelPredictionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "predict",
                request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content
                .ReadFromJsonAsync<FuelPredictionResponse>();
        }
    }
}