
using HNGStageThree.API.Models.Domain;
using System.Text.Json;

namespace HNGStageThree.API.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly HttpClient httpClient;

        public NewsRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<NewsResponse?> FetchNews()
        {
            try
            {
                string url = "https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey=5a3e2c309c8a40599e61085b684ff5e8";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var newsResponse = JsonSerializer.Deserialize<NewsResponse>(
                    jsonResponse,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                return newsResponse;
            }
            catch
            {
                return null;
            }
        }
    }
}
