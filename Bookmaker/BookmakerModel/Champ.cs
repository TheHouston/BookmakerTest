using Bookmaker.DbModel;
using System.Collections.Generic;

namespace Bookmaker.BookmakerModel
{
    public class Champ
    {
        public int ChampId { get; set; }
        public string ChampName { get; set; }
        public int EventsQuantity { get; set; }
        public IEnumerable<Event> Events { get; set; }

    }
}
