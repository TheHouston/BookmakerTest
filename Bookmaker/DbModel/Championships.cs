using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookmaker.DbModel
{
    public partial class Championships
    {
        public Championships()
        {
            Events = new HashSet<Events>();
        }
        [Key]
        public int ChampId { get; set; }
        public int SportId { get; set; }
        public string ChampName { get; set; }

        public Sports Sport { get; set; }
        public ICollection<Events> Events { get; set; }
    }
}
