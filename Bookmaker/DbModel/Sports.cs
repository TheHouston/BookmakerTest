using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookmaker.DbModel
{
    public partial class Sports
    {
        public Sports()
        {
            Championships = new HashSet<Championships>();
            Teams = new HashSet<Teams>();
        }
        [Key]
        public int SportId { get; set; }
        public string SportName { get; set; }

        public ICollection<Championships> Championships { get; set; }
        public ICollection<Teams> Teams { get; set; }
    }
}
