using FokinClicker.Domain;
using FokinClicker.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FokinClicker.Initializer;

namespace FokinClicker;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);
        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DbContextInitialiser.InitializeDbContext(appDbContext);

        app.MapGet("/", () => "Hello World!");
        app.MapHealthChecks("health-check");

        app.Run();
        }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        DbContextInitialiser.AddAppDbContext(services);
    }
}

