namespace FokinClicker.UseCases.GetSupports;

public class SupportDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public long Price { get; init; }
    public int Multiplier { get; init; }
    public int BoostId { get; init; }
    public byte[] Image { get; init; }
}
