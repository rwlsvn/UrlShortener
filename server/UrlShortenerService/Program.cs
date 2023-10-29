using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Data;
using UrlShortener.Library.DependencyInjection;
using UrlShortenerService.Repositories;
using UrlShortenerService.Services;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{    
    services.AddControllers();

    services.AddDbContext<UrlsDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration
            .GetConnectionString("UrlsDbConnection"));
    });

    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

    services.AddScoped<IShortenedUrlsRepository, ShortenedUrlsRepository>();
    services.AddScoped<IShortUrlGenerator, ShortUrlGenerator>();

    services.AddJwtAuth(builder.Configuration);
}

void Configure(IApplicationBuilder app)
{
    app.UseCors("AllowAll");

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    
    app.UseStaticFiles();
}