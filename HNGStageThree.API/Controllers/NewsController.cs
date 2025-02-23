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
            string result;
            NewsResponse? res = await newsRepository.FetchNews();

            // Check if res or its Articles property is null or empty
            if (res?.Articles == null || !res.Articles.Any())
            {
                result = "No articles found at this time. Check back in an hour";
                await SendWebhook(result);

                return Ok(result); 
            }

            var firstArticle = res.Articles.FirstOrDefault();
            if (firstArticle == null)
            {
                result = "No articles available.";
            }
            else
            {
                result = $"Headline for the hour: {firstArticle.Title}. Link: {firstArticle.Url}";
            }

            await SendWebhook(result);
            return Ok(result);
        }

        private async Task SendWebhook(string result)
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

            Console.WriteLine(responseBody);
        }




    }
}
