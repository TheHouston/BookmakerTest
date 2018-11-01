using System.Collections.Generic;

namespace Bookmaker.BookmakerModel
{
    public class Sport
    {
        public int SportId { get; set; }
        public string SportName { get; set; }
        public int EventsQuantity { get; set; }
        public IEnumerable<Champ> Champs { get; set; }
    }
}
