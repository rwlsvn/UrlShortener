namespace UrlShortenerService.Models.Requests
{
    public class AddUrlRequest
    {
        public Guid UserId { get; set; }
        public string OriginalUrl { get; set; }
        public string? Description {  get; set; } 
    }
}
