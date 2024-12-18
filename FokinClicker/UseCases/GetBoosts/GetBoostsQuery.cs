using MediatR;

namespace FokinClicker.UseCases.GetBoosts;

public record GetBoostsQuery : IRequest<IReadOnlyCollection<BoostDto>>;