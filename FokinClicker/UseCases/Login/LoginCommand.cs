using MediatR;
namespace FokinClicker.UseCases.Login;
public record LoginCommand(string UserName, string Password) : IRequest<Unit>;