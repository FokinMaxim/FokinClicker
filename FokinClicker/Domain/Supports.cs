namespace FokinClicker.Domain
{
    public class Supports
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public long Price { get; set; }

        public int Multiplier { get; set; }
        public int BoostId { get; set; }

        public byte[] Image { get; set; }
    }
}
