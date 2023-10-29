using System.Threading.Tasks;
using UrlShortenerService.Models;
using UrlShortenerService.Models.Dto;
using UrlShortenerService.Models.Requests;
using UrlShortenerService.Models.Results;

namespace UrlShortenerService.Repositories
{
    public interface IShortenedUrlsRepository
    {
        Task<ShortenedUrl> GetUrlDetails(Guid id);
        Task<string> GetOriginalUrl(string shortUrl);
        Task<ICollection<ShortenedUrlDto>> All();
        Task<AddUrlResult> Add(AddUrlRequest addUrlDto);
        Task<DeleteUrlResult> Delete(Guid urlId);
        Task<DeleteUrlResult> Delete(Guid urlId, Guid userId);

    }
}
