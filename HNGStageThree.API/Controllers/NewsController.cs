using HNGStageThree.API.Models.Domain;
using HNGStageThree.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HNGStageThree.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        [HttpPost]
        [Route("tick")]
        public async Task<IActionResult> FetchNews()
        {
            NewsResponse? res = await newsRepository.FetchNews();

            if (res?.Articles == null || !res.Articles.Any())
            {
                return NotFound("No articles found.");
            }

            var firstArticle = res.Articles.First();
            string result = $"Headline for the hour: {firstArticle.Title}. Link: {firstArticle.Url}";

            SendWebhook(result);



            return Ok(result);
        }

        private async void SendWebhook(string result)
        {
            HttpClient client = new HttpClient();


            string webhookUrl = Environment.GetEnvironmentVariable("WEBHOOK_API") ?? "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, webhookUrl);

            request.Headers.Add("accept", "application/json");

            request.Content = new StringContent("{\n    \"event_name\": \"Tech news monitor\",\n   \"message\": \"" + result + "\",\n    \"status\": \"success\",\n    \"username\": \"Tech news bot\"\n  }");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }




    }
}
