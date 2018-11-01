using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookmaker.DbModel
{
    public partial class Events
    {
        public Events()
        {
            Bets = new HashSet<Bets>();
        }
        [Key]
        public int EventId { get; set; }
        public int ChampId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public double HomePrice { get; set; }
        public double DrawPrice { get; set; }
        public double AwayPrice { get; set; }
        public DateTime EventDate { get; set; }

        public Teams AwayTeam { get; set; }
        public Championships Champ { get; set; }
        public Teams HomeTeam { get; set; }
        public ICollection<Bets> Bets { get; set; }
    }
}
