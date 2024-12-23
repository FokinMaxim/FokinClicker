﻿using AutoMapper;
using FokinClicker.DomainServices;
using FokinClicker.Infrastructure.Abstractions;
using FokinClicker.UseCases.AddPoints;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.UseCases.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
{
	private readonly ICurrentUserAccessor currentUserAccessor;
	private readonly IAppDbContext appDbContext;
	private readonly IMapper mapper;

	public GetCurrentUserQueryHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext, IMapper mapper)
	{
		this.currentUserAccessor = currentUserAccessor;
		this.appDbContext = appDbContext;
		this.mapper = mapper;
	}

	public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
	{
		var userId = currentUserAccessor.GetCurrentUserId();

		var user = await appDbContext.ApplicationUsers
			.Include(user => user.UserBoosts)
			.ThenInclude(ub => ub.Boost)
            .Include(user => user.UserSupports)
			.ThenInclude(us => us.Support)
            .FirstAsync(user => user.Id == userId);

		var userDto = mapper.Map<UserDto>(user);

		userDto.ProfitPerClick = user.GetUserProfit();
		userDto.ProfitPerSecond = user.GetUserProfit(shouldCalculateAutoBoosts: true);

		return userDto;
	}
}
