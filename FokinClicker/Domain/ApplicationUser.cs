using Microsoft.AspNetCore.Identity;

namespace FokinClicker.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        //public Guid ScoreId { get; set; }

        public long CurrentScore { get; set; }

        public long RecordScore { get; set; }

        public IEnumerable<UserBoots> UserBoosts { get; set; } = [];
    }
}
