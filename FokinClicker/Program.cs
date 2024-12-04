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

        app.MapGet("/", () => "Hello World!");
        app.MapHealthChecks("health-check");

        app.Run();
        }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        DbContextInitialiser.InitializeDbContext(services);
    }
}

