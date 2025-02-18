using HNGStageThree.API.Models.Domain;

namespace HNGStageThree.API.Repository
{
    public interface INewsRepository
    {
        Task<NewsResponse?> FetchNews();
    }
}
