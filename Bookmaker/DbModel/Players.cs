using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookmaker.DbModel
{
    public partial class Players
    {
        public Players()
        {
            Bets = new HashSet<Bets>();
        }
        [Key]
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int PaymentId { get; set; }
        public string Password { get; set; }

        public ICollection<Bets> Bets { get; set; }
    }
}
