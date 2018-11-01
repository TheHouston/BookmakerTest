using System.ComponentModel.DataAnnotations;

namespace Bookmaker.DbModel
{
    public partial class Bets
    {
        [Key]
        public int BetId { get; set; }
        public int EventId { get; set; }
        public int PlayerId { get; set; }
        public decimal Stake { get; set; }
        public double Price { get; set; }
        public int? TeamId { get; set; }

        public Events Event { get; set; }
        public Players Player { get; set; }
        public Teams Team { get; set; }
    }
}
