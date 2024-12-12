using MediatR;
namespace FokinClicker.UseCases.Register;

public record RegisterCommand(string UserName, string Password): IRequest<Unit>;

