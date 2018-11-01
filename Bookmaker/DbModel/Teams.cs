using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookmaker.DbModel
{
    public partial class Teams
    {
        public Teams()
        {
            Bets = new HashSet<Bets>();
            EventsAwayTeam = new HashSet<Events>();
            EventsHomeTeam = new HashSet<Events>();
        }
        [Key]
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int SportId { get; set; }

        public Sports Sport { get; set; }
        public ICollection<Bets> Bets { get; set; }
        public ICollection<Events> EventsAwayTeam { get; set; }
        public ICollection<Events> EventsHomeTeam { get; set; }
    }
}
