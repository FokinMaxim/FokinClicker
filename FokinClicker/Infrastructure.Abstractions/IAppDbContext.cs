using FokinClicker.Domain;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.Infrastructure.Abstractions;

public interface IAppDbContext
{
    DbSet<ApplicationRole> ApplicationRoles { get; }
    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<Boost> Boosts { get; }
    DbSet<UserBoost> UserBoosts { get; }
    DbSet<Supports> Supports { get; }
    DbSet<UserSupport> UserSupports { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
