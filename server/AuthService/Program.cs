using AuthService.Configuration;
using AuthService.Data;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Library.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
Configure(app);

app.Run();


void RegisterServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddDbContext<UsersDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration
            .GetConnectionString("UsersDbConnection"));
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

    services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
        .AddEntityFrameworkStores<UsersDbContext>()
        .AddDefaultTokenProviders();

    services.AddScoped<ITokenBuilderService, TokenBuilderService>();

    services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));

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
}