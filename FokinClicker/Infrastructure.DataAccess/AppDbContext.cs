using FokinClicker.Domain;
using FokinClicker.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.Infrastructure.DataAccess;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IAppDbContext
{
    public DbSet<ApplicationRole> ApplicationRoles {  get; private set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; private set; }

    public DbSet<Boost> Boosts { get; private set; }
    public DbSet<UserBoost> UserBoosts { get; private set; }
    public DbSet<Supports> Supports { get; private set; }
    public DbSet<UserSupport> UserSupports { get; private set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserBoost>()
            .HasOne(ub => ub.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder.Entity<UserBoost>()
             .HasOne(ub => ub.Boost)
             .WithMany()
             .HasForeignKey(ub => ub.BoostId);

        builder.Entity<UserSupport>()
            .HasOne(ub => ub.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder.Entity<UserSupport>()
             .HasOne(ub => ub.Support)
             .WithMany()
             .HasForeignKey(ub => ub.SupportId);

        builder.Entity<UserSupport>()
             .HasOne(ub => ub.UserBoost)
             .WithMany()
             .HasForeignKey(ub => ub.UserBoostId);
    }
}

