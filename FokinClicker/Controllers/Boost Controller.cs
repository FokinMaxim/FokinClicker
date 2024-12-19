using FokinClicker.UseCases.BuyBoosts;
using FokinClicker.UseCases.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FokinClicker.Controllers;

[Route("boost")]
public class BoostController : ControllerBase
{
	private readonly IMediator mediator;

	public BoostController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[HttpPost("buy")]
	public async Task<ScoreBoostDto> Buy(BuyBoostCommand command)
		=> await mediator.Send(command);
}
