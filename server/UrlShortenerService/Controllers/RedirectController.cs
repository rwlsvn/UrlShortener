using Microsoft.AspNetCore.Mvc;
using UrlShortenerService.Controllers.Base;
using UrlShortenerService.Repositories;

namespace UrlShortenerService.Controllers
{
    [Route("su")]
    public class RedirectController : BaseController
    {
        private IShortenedUrlsRepository _urlsRepository;

        public RedirectController(IShortenedUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }

        [Route("{shortUrl}")]
        public async Task<IActionResult> RedirectByShortUrl(string shortUrl)
        {
            string url = await _urlsRepository.GetOriginalUrl(shortUrl);

            if(url == null)
            {
                return NotFound();
            }

            return Redirect(url);
        }
    }
}
