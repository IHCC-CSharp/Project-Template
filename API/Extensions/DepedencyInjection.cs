using System.Data;
using API.Data.Repositories;
using API.Services;
using Microsoft.Data.Sqlite;

namespace API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDbConnection>(sp => new SqliteConnection(connectionString));
        
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILaptopRepository, LaptopRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }
}