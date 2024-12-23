using FokinClicker.UseCases.BuyBoosts;
using FokinClicker.UseCases.BuySupports;
using FokinClicker.UseCases.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FokinClicker.Controllers;

[Route("support")]
public class SupportController : ControllerBase
{
    private readonly IMediator mediator;

    public SupportController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("buy")]
    public async Task<ScoreSupportDto> Buy(BuySupportCommand command)
        => await mediator.Send(command);
}