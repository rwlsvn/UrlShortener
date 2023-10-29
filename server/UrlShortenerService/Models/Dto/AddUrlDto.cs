namespace UrlShortenerService.Models.Dto
{
    public class AddUrlDto
    {
        public string OriginalUrl { get; set; }
        public string? Description { get; set; }
    }
}
