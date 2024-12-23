using FokinClicker.Domain;

namespace FokinClicker.DomainServices;

public static class BoostsProfitCalculationExtensions
{
    public static long GetProfit(this IEnumerable<UserBoost> userBoosts, bool shouldCalculateAutoBoosts = false)
    {
        if (shouldCalculateAutoBoosts)
        {
            return userBoosts
                .Where(ub => ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit);
        }
        return 1 + userBoosts
                .Where(ub => !ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit);
    }

    public static long GetUserProfit(this ApplicationUser user, bool shouldCalculateAutoBoosts = false)
    {
        if (shouldCalculateAutoBoosts)
        {
            return user.UserBoosts
                .Where(ub => ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit);
        }
        return 1 + user.UserBoosts
                .Where(ub => !ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit *
                    GetMultiplier(user.UserSupports, ub.BoostId));
    }

    public static int GetMultiplier(IEnumerable<UserSupport> userSupports, int boostId)
    {
        var existingSupports = userSupports.Where(us => us.Support.BoostId == boostId);
        if (existingSupports.ToList().Count > 0)
        {
            return existingSupports.First().Support.Multiplier * existingSupports.First().Quantity;
        }
        return 1;
    }
}