using FokinClicker.UseCases.GetBoosts;
using FokinClicker.UseCases.GetCurrentUser;
using FokinClicker.UseCases.GetSupports;

namespace FokinClicker.ViewModels;

public class IndexViewModel
{
    public UserDto User { get; init; }
    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
    public IReadOnlyCollection<SupportDto> Supports { get; init; }
}
