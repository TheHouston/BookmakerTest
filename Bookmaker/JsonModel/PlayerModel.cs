using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Bookmaker.JsonModel
{
    public class PlayerModel
    {
        [Required, JsonProperty("login")]
        public string Login { get; set; }
        [Required, JsonProperty("password")]
        public string Password { get; set; }
    }
}
