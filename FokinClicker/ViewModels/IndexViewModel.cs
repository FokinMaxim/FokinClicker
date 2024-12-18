using FokinClicker.UseCases.GetBoosts;
using FokinClicker.UseCases.GetCurrentUser;

namespace FokinClicker.ViewModels;

public class IndexViewModel
{
    public UserDto User { get; init; }
    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}
