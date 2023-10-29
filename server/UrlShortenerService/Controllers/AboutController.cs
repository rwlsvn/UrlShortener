using Microsoft.AspNetCore.Mvc;
using UrlShortenerService.Controllers.Base;
using System.IO;
using UrlShortenerService.Models;

namespace UrlShortenerService.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : BaseController
    {
        private readonly IWebHostEnvironment _env;

        public AboutController(IWebHostEnvironment env) 
        {
            _env = env;
        }

        [HttpPost]
        public async Task About(AboutText aboutText)
        {
            var savePath = Path.Combine(_env.WebRootPath, "about.txt");
            try
            {
                await System.IO.File.WriteAllTextAsync(savePath, aboutText.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
