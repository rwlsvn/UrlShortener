using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerService.Controllers.Base;
using UrlShortenerService.Models;
using UrlShortenerService.Models.Dto;
using UrlShortenerService.Models.Requests;
using UrlShortenerService.Models.Results;
using UrlShortenerService.Repositories;

namespace UrlShortenerService.Controllers
{
    [Route("api/user/shortenerurl")]
    public class UserShortenerUrlController : BaseController
    {
        private IShortenedUrlsRepository _urlsRepository;

        public UserShortenerUrlController(IShortenedUrlsRepository urlsRepository) 
        {
            _urlsRepository = urlsRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IList<ShortenedUrlDto>>> AllShortUrls()
        {
            var shortUrls = await _urlsRepository.All();
            return Ok(shortUrls);
        }

        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ShortenedUrl>> Add(Guid id)
        {
            var response = await _urlsRepository.GetUrlDetails(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<AddUrlResult>> Add(AddUrlDto addUrlDto)
        {
            var addUrlrequest = new AddUrlRequest
            {
                UserId = UserId,
                OriginalUrl = addUrlDto.OriginalUrl,
                Description = addUrlDto?.Description
            };
            var response = await _urlsRepository.Add(addUrlrequest);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<AddUrlResult>> Delete(Guid id)
        {
            var response = await _urlsRepository.Delete(id, UserId);
            return Ok(response);
        }
    }
}
