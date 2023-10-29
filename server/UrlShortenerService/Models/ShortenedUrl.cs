namespace UrlShortenerService.Models
{
    public class ShortenedUrl
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string OriginalUrl { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
