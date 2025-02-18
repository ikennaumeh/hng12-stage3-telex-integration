
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
                string url = "https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey=b4fb0b3dce6a42338bdaa47f9b882185";
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
