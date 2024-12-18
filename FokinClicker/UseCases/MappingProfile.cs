using AutoMapper;
using FokinClicker.Domain;
using FokinClicker.UseCases.GetBoosts;
using FokinClicker.UseCases.GetCurrentUser;

namespace FokinClicker.UseCases;

public class MappingProfie : Profile
{
    public MappingProfie()
    {
        CreateMap<Boost, BoostDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<ApplicationUser, UserDto>();
    }
}