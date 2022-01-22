using Microsoft.EntityFrameworkCore;
using UdemusDateus.Data;
using UdemusDateus.Interfaces;
using UdemusDateus.Services;

namespace UdemusDateus.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}