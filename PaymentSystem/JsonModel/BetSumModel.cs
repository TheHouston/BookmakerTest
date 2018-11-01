using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.JsonModel
{
    public class BetSumModel
    {
        [Required, JsonProperty("user_id")]
        public int UserId { get; set; }
        [Required, JsonProperty("bet_sum")]
        public decimal BetSum { get; set; }
    }
}
