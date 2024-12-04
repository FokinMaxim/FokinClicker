using FokinClicker.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.Infrastructure.DataAccess;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public DbSet<ApplicationRole> ApplicationRoles {  get; private set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; private set; }

    public DbSet<Boost> Boosts { get; private set; }
    public DbSet<UserBoots> UserBoosts { get; private set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserBoots>()
            .HasOne(ub => ub.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder.Entity<UserBoots>()
             .HasOne(ub => ub.Boost)
             .WithMany()
             .HasForeignKey(ub => ub.BoostId);
    }
}

