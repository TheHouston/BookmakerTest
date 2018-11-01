
using System;

namespace Bookmaker.BookmakerModel
{
    public class Event
    {
        public int EventId { get; set; }
        public int ChampId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public double HomePrice { get; set; }
        public double DrawPrice { get; set; }
        public double AwayPrice { get; set; }
        public DateTime EventDate { get; set; }
    }
}
