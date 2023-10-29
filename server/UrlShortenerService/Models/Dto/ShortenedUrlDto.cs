namespace UrlShortenerService.Models.Dto
{
    public class ShortenedUrlDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string OriginalUrl { get; set; }
    }
}
