namespace FokinClicker.Domain
{
    public class UserSupport
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int SupportId { get; set; }

        public Supports Support { get; set; }

        public long CurrentPrice { get; set; }

        public int Quantity { get; set; }
    }
}
