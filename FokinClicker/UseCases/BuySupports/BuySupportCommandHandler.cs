using FokinClicker.Domain;
using FokinClicker.DomainServices;
using FokinClicker.Infrastructure.Abstractions;
using FokinClicker.UseCases.BuyBoosts;
using FokinClicker.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.UseCases.BuySupports;

public class BuySupportCommandHandler : IRequestHandler<BuySupportCommand, ScoreSupportDto>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;

    public BuySupportCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<ScoreSupportDto> Handle(BuySupportCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();
        var user = await appDbContext.ApplicationUsers
             .Include(user => user.UserBoosts)
             .ThenInclude(ub => ub.Boost)
             .Include(user => user.UserSupports)
             .ThenInclude(ub => ub.Support)
             .FirstAsync(user => user.Id == userId, cancellationToken);

        var support = await appDbContext.Supports
            .FirstAsync(b => b.Id == request.SupportId, cancellationToken);

        var existingUserSupport = user.UserSupports.FirstOrDefault(ub => ub.SupportId == request.SupportId);

        var price = 0L;

        UserSupport userSupport = existingUserSupport!;
        if (existingUserSupport != null)
        {
            price = existingUserSupport.CurrentPrice;
            existingUserSupport.Quantity++;
            existingUserSupport.CurrentPrice = Convert.ToInt64(existingUserSupport.CurrentPrice * DomainConstants.BoostCostModifier);
        }
        else
        {
            price = support.Price;
            var newUserSupport = new UserSupport()
            {
                Support = support,
                CurrentPrice = Convert.ToInt64(support.Price * DomainConstants.BoostCostModifier),
                Quantity = 1,
                User = user,
            };

            userSupport = newUserSupport;
            await appDbContext.UserSupports.AddAsync(newUserSupport, cancellationToken);
        }

        if (price > user.CurrentScore)
        {
            throw new InvalidOperationException("Not enough score to buy a Support.");
        }

        user.CurrentScore -= price;

        await appDbContext.SaveChangesAsync(cancellationToken);

        return new ScoreSupportDto
        {
            Score = new ScoreDto
            {
                CurrentScore = user.CurrentScore,
                RecordScore = user.RecordScore,
                ProfitPerClick = user.GetUserProfit(),
                ProfitPerSecond = user.GetUserProfit(shouldCalculateAutoBoosts: true)
            },
            Price = userSupport.CurrentPrice,
            Quantity = userSupport.Quantity,
        };
    }
}