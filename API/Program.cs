using System.Data;
using Microsoft.Data.Sqlite;
using API.Data.Repositories;
using API.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=CampusLocker.db";

builder.Services.AddScoped<IDbConnection>(sp => new SqliteConnection(connectionString));

builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

builder.Services.AddControllers();

// Repositories
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();

// Services
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IMemberService, MemberService>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

using (var scope = app.Services.CreateScope())
{
    var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
    await DbInitializer.Initialize(connectionString);
}

app.MapControllers();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
