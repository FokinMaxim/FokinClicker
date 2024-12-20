namespace FokinClicker.UseCases.GetSupports;

public class SupportDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public long Price { get; init; }
    public long Multiplier { get; init; }
    public byte[] Image { get; init; }
}
