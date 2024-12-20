using MediatR;

namespace FokinClicker.UseCases.GetSupports;

public record GetSupportsQuery : IRequest<IReadOnlyCollection<SupportDto>>;
