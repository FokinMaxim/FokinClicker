namespace FokinClicker.UseCases.Common;

public class ScoreSupportDto
{
    public required ScoreDto Score { get; init; }

    public int Quantity { get; init; }

    public long Price { get; init; }
}
