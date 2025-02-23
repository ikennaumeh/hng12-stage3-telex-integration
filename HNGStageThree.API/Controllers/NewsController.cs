using HNGStageThree.API.Models.Domain;
using HNGStageThree.API.Repository;
using Microsoft.AspNetCore.Mvc;

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
            NewsResponse? response = await newsRepository.FetchNews();

            if (response?.Articles == null || !response.Articles.Any())
            {
                return NotFound("No articles found.");
            }

            var firstArticle = response.Articles.First();
            string result = $"Headline for the hour: {firstArticle.Title}. Link: {firstArticle.Url}";

            return Ok(result);
        }
    }
}
