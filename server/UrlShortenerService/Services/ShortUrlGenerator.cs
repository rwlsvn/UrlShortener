using System.Text;

namespace UrlShortenerService.Services
{
    public class ShortUrlGenerator : IShortUrlGenerator
    {
        public string GenerateShortUrl()
        {
            var random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            var symbols = "abcdefghijklmnopqrstuvwxyz0123456789";
            
            for (int i = 0; i < 8; i++)
            {
                char randomChar = symbols[random.Next(symbols.Length)];
                stringBuilder.Append(randomChar);
            }

            string result = stringBuilder.ToString();

            return result;
        }
    }
}
