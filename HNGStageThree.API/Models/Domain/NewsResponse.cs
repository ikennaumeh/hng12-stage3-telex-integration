namespace HNGStageThree.API.Models.Domain
{
    public class NewsResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public Article[] Articles { get; set; }
    }
}
