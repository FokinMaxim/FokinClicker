using MediatR;

namespace FokinClicker.UseCases.AddPoints;

public record AddPointsCommand(int Times, bool IsAuto = false) : IRequest<Unit>;