namespace UrlShortenerService.Models.Results
{
    public class AddUrlResult
    {
        public bool Succeeded { get; set; }
        public ICollection<ErrorInfo> Errors { get; set; }
            = new List<ErrorInfo>();
        public Guid? Id { get; set; }
    }
}
