using MediatR;

namespace FokinClicker.UseCases.Logout;

public record LogoutCommand : IRequest<Unit>;
