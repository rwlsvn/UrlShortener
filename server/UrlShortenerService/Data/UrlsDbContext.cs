using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UrlShortenerService.Models;

namespace UrlShortenerService.Data
{
    public class UrlsDbContext : DbContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public UrlsDbContext(DbContextOptions<UrlsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
