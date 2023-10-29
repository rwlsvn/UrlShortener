using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Data;
using UrlShortenerService.Models;
using UrlShortenerService.Models.Dto;
using UrlShortenerService.Models.Requests;
using UrlShortenerService.Models.Results;
using UrlShortenerService.Services;

namespace UrlShortenerService.Repositories
{
    public class ShortenedUrlsRepository : IShortenedUrlsRepository
    {
        private readonly UrlsDbContext _context;
        private readonly IShortUrlGenerator _urlShortener;

        public ShortenedUrlsRepository(UrlsDbContext context, 
            IShortUrlGenerator urlShortener)
        {
            _context = context;
            _urlShortener = urlShortener;
        }

        public async Task<ICollection<ShortenedUrlDto>> All()
        {
            var shortenedUrls = _context.ShortenedUrls
                .Select(s => new ShortenedUrlDto
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Url = s.Url,
                    OriginalUrl = s.OriginalUrl
                })
                .ToList();

            return shortenedUrls;
        }

        public async Task<string> GetOriginalUrl(string shortUrl)
        {
            var urlModel = await _context.ShortenedUrls
                .FirstOrDefaultAsync(x => x.Url == shortUrl);

            return urlModel.OriginalUrl;
        }

        public async Task<ShortenedUrl> GetUrlDetails(Guid id)
        {
            var urlModel = await _context.ShortenedUrls
                .FirstOrDefaultAsync(x => x.Id == id);

            return urlModel;
        }

        public async Task<AddUrlResult> Add(AddUrlRequest addUrlDto)
        {
            var shortenedUrl = await _context.ShortenedUrls
                .FirstOrDefaultAsync(x => x.OriginalUrl == addUrlDto.OriginalUrl);

            if(shortenedUrl != null)
            {
                var addUrlResult = new AddUrlResult
                {
                    Succeeded = false
                };
                addUrlResult.Errors.Add(new ErrorInfo
                {
                    Code = "DuplicateUrl",
                    Description = $"URL '{addUrlDto.OriginalUrl}' is already exist"
                });
                return addUrlResult;
            }

            var newShortenedUrl = new ShortenedUrl
            {
                Id = Guid.NewGuid(),
                UserId = addUrlDto.UserId,
                OriginalUrl = addUrlDto.OriginalUrl,
                Url = _urlShortener.GenerateShortUrl(),
                Description = addUrlDto?.Description,
                CreationDate = DateTime.Now
            };

            await _context.ShortenedUrls.AddAsync(newShortenedUrl);
            await _context.SaveChangesAsync();

            return new AddUrlResult
            {
                Succeeded = true,
                Id = newShortenedUrl.Id
            };
        }

        public async Task<DeleteUrlResult> Delete(Guid urlId)
        {
            var shortenedUrl = await _context.ShortenedUrls
                .FirstOrDefaultAsync(x => x.Id == urlId);

            if(shortenedUrl == null) 
            {
                var deleteUrlResult = new DeleteUrlResult
                {
                    Succeeded = false
                };
                deleteUrlResult.Errors.Add(new ErrorInfo
                {
                    Code = "UrlNotFound",
                    Description = $"URL '{urlId}' does not exist."
                });
                return deleteUrlResult;
            }

            _context.ShortenedUrls.Remove(shortenedUrl);
            await _context.SaveChangesAsync();
            return new DeleteUrlResult { Succeeded = true };
        }

        public async Task<DeleteUrlResult> Delete(Guid urlId, Guid userId)
        {
            var shortenedUrl = await _context.ShortenedUrls
                .FirstOrDefaultAsync(x => x.Id == urlId && x.UserId == userId);

            if (shortenedUrl == null)
            {
                var deleteUrlResult = new DeleteUrlResult
                {
                    Succeeded = false
                };
                deleteUrlResult.Errors.Add(new ErrorInfo
                {
                    Code = "PermissionDenied",
                    Description = $"User '{userId}' does not have permission " +
                    $"to delete URL '{urlId}'."
                });
                return deleteUrlResult;
            }

            _context.ShortenedUrls.Remove(shortenedUrl);
            await _context.SaveChangesAsync();

            return new DeleteUrlResult { Succeeded = true };
        }
    }
}
