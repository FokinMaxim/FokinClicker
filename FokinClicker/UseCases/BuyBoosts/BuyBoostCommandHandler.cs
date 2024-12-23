using FokinClicker.Domain;
using FokinClicker.DomainServices;
using FokinClicker.Infrastructure.Abstractions;
using FokinClicker.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.UseCases.BuyBoosts;

public class BuyBoostCommandHandler : IRequestHandler<BuyBoostCommand, ScoreBoostDto>
{
	private readonly ICurrentUserAccessor currentUserAccessor;
	private readonly IAppDbContext appDbContext;

	public BuyBoostCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
	{
		this.currentUserAccessor = currentUserAccessor;
		this.appDbContext = appDbContext;
	}

	public async Task<ScoreBoostDto> Handle(BuyBoostCommand request, CancellationToken cancellationToken)
	{
		var userId = currentUserAccessor.GetCurrentUserId();
		var user = await appDbContext.ApplicationUsers
			.Include(user => user.UserBoosts)
			.ThenInclude(ub => ub.Boost)
            .Include(user => user.UserSupports)
            .ThenInclude(ub => ub.Support)
            .FirstAsync(user => user.Id == userId, cancellationToken);
		var boost = await appDbContext.Boosts
			.FirstAsync(b => b.Id == request.BoostId, cancellationToken);

		var existingUserBoost = user.UserBoosts.FirstOrDefault(ub => ub.BoostId == request.BoostId);

		var price = 0L;

		UserBoost userBoost = existingUserBoost!;
		if (existingUserBoost != null)
		{
			price = existingUserBoost.CurrentPrice;
			existingUserBoost.Quantity++;
			existingUserBoost.CurrentPrice = Convert.ToInt64(existingUserBoost.CurrentPrice * DomainConstants.BoostCostModifier);
		}
		else
		{
			price = boost.Price;
			var newUserBoost = new UserBoost()
			{
				Boost = boost,
				CurrentPrice = Convert.ToInt64(boost.Price * DomainConstants.BoostCostModifier),
				Quantity = 1,
				User = user,
			};

			userBoost = newUserBoost;
			await appDbContext.UserBoosts.AddAsync(newUserBoost, cancellationToken);
		}

		if (price > user.CurrentScore)
		{
			throw new InvalidOperationException("Not enough score to buy a boost.");
		}

		user.CurrentScore -= price;

		await appDbContext.SaveChangesAsync(cancellationToken);

		return new ScoreBoostDto
		{
			Score = new ScoreDto
			{
				CurrentScore = user.CurrentScore,
				RecordScore = user.RecordScore,
				ProfitPerClick = user.GetUserProfit(),
				ProfitPerSecond = user.GetUserProfit(shouldCalculateAutoBoosts: true)
			},
			Price = userBoost.CurrentPrice,
			Quantity = userBoost.Quantity,
		};
	}
}
