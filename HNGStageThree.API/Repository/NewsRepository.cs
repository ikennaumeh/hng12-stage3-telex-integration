
using HNGStageThree.API.Models.Domain;
using System.Text.Json;

namespace HNGStageThree.API.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public NewsRepository(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }
        public async Task<NewsResponse?> FetchNews()
        {
            try
            {
                string url = $"https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey={configuration["ApiKey:2"]}";
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
