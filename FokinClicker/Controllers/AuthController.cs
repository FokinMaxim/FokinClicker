using FokinClicker.UseCases.Login;
using FokinClicker.UseCases.Logout;
using FokinClicker.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FokinClicker.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IMediator mediator;
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}
