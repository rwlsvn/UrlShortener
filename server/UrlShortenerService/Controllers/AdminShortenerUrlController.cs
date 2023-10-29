using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerService.Controllers.Base;
using UrlShortenerService.Models.Results;
using UrlShortenerService.Repositories;

namespace UrlShortenerService.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/shortenerurl")]
    public class AdminShortenerUrlController : BaseController
    {
        private IShortenedUrlsRepository _urlsRepository;

        public AdminShortenerUrlController(IShortenedUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<AddUrlResult>> Delete(Guid id)
        {
            var response = await _urlsRepository.Delete(id);
            return Ok(response);
        }
    }
}
