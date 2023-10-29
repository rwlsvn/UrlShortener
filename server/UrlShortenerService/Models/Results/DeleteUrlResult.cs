namespace UrlShortenerService.Models.Results
{
    public class DeleteUrlResult
    {
        public bool Succeeded { get; set; }
        public ICollection<ErrorInfo> Errors { get; set; } 
            = new List<ErrorInfo>();
    }
}
