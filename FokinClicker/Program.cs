using FokinClicker.Domain;
using FokinClicker.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FokinClicker.Initializer;
using CSharpClicker.Web.Initializers;
using FokinClicker.Infrastructure.Abstractions;
using FokinClicker.Infrastructure.Implementations;

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

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

		app.UseStaticFiles();
		app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();
        app.MapDefaultControllerRoute();
        app.MapHealthChecks("health-check");

        app.Run();
        }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddAuthentication()
			.AddCookie(o => o.LoginPath = "/auth/login"); ;
        services.AddAuthorization();
        services.AddControllersWithViews();

		services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
		services.AddScoped<IAppDbContext, AppDbContext>();

        IdentityInitializer.AddIdentity(services);
        DbContextInitialiser.AddAppDbContext(services);
    }
}

