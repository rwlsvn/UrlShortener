using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortenerService.Models;

namespace UrlShortenerService.EntityTypeConfiguration
{
    public class ShortenedUrlConfiguration : IEntityTypeConfiguration<ShortenedUrl>
    {
        public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Url).HasMaxLength(8);
            builder.Property(x => x.Description).HasMaxLength(256);
        }
    }
}
