using MediatR;

namespace FokinClicker.UseCases.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserDto>;
