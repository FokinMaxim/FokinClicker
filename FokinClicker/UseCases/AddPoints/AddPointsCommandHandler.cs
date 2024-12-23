using FokinClicker.DomainServices;
using FokinClicker.Infrastructure.Abstractions;
using FokinClicker.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.UseCases.AddPoints;

public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand, ScoreDto>
{
	private readonly ICurrentUserAccessor currentUserAccessor;
	private readonly IAppDbContext appDbContext;

	public AddPointsCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
	{
		this.currentUserAccessor = currentUserAccessor;
		this.appDbContext = appDbContext;
	}

	public async Task<ScoreDto> Handle(AddPointsCommand request, CancellationToken cancellationToken)
	{
		var userId = currentUserAccessor.GetCurrentUserId();
		var user = await appDbContext.ApplicationUsers
			.Include(user => user.UserBoosts)
			.ThenInclude(ub => ub.Boost)
            .Include(user => user.UserSupports)
            .ThenInclude(ub => ub.Support)
            .FirstAsync(user => user.Id == userId);

		var profitPerSecond = user.GetUserProfit(shouldCalculateAutoBoosts: true);
		var profitPerClick = user.GetUserProfit();

		var autoPoints = profitPerSecond * request.Seconds;
		var clickedPoints = profitPerClick * request.Clicks;

		user.CurrentScore += autoPoints + clickedPoints;
		user.RecordScore += autoPoints + clickedPoints;

		await appDbContext.SaveChangesAsync();

		return new ScoreDto
		{
			CurrentScore = user.CurrentScore,
			RecordScore = user.RecordScore,
			ProfitPerClick = profitPerClick,
			ProfitPerSecond = profitPerSecond,
		};
	}
}