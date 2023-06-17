using Beers.Models;
using System.Text.Json;

namespace Beers.Services
{
  
    public class PunkApiService : IPunkApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PunkApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<Beer>> GetMenu()
        {
            var response = await _httpClient.GetAsync("beers");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var beers = await JsonSerializer.DeserializeAsync<List<Beer>>(contentStream, _jsonOptions);
                return beers;
            }
            return Enumerable.Empty<Beer>();
        }

        public async Task<Beer> GetBeerById(int id)
        {
            var response = await _httpClient.GetAsync($"beers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var beers = await JsonSerializer.DeserializeAsync<List<Beer>>(contentStream, _jsonOptions);
                return beers.FirstOrDefault();
            }
            return null;
        }

        public async Task<Beer> GetRandomBeer()
        {
            var response = await _httpClient.GetAsync("beers/random");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var beers = await JsonSerializer.DeserializeAsync<List<Beer>>(contentStream, _jsonOptions);
                return beers.FirstOrDefault();
            }
            return null;
        }

        public async Task<IEnumerable<Beer>> SearchBeers(string query)
        {
            var encodedQuery = Uri.EscapeDataString(query);
            var response = await _httpClient.GetAsync($"beers?beer_name={encodedQuery}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var beers = await JsonSerializer.DeserializeAsync<List<Beer>>(contentStream, _jsonOptions);
                return beers;
            }
            return Enumerable.Empty<Beer>();
        }
    }

}
