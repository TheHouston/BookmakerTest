using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Bookmaker.JsonModel
{
    public class BetModel
    {
        [Required, JsonProperty("eventId")]
        public int EventId { get; set; }
        [Required, JsonProperty("teamId")]
        public int TeamId { get; set; }
        [Required, JsonProperty("price")]
        public double Price { get; set; }

        [Required, JsonProperty("betSum")]
        public decimal BetSum { get; set; }
    }
}
