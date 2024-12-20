using AutoMapper;
using FokinClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.UseCases.GetSupports;

public class GetSupportsQueryHandler : IRequestHandler<GetSupportsQuery, IReadOnlyCollection<SupportDto>>
{
    private IAppDbContext appDbContext;
    private IMapper mapper;
    public GetSupportsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }
    public async Task<IReadOnlyCollection<SupportDto>> Handle(GetSupportsQuery request, CancellationToken cancellationToken)
    {
        return await mapper
            .ProjectTo<SupportDto>(appDbContext.Boosts)
            .ToArrayAsync();
    }
}
