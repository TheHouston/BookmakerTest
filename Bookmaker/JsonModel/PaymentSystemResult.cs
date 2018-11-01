using Newtonsoft.Json;

namespace Bookmaker.JsonModel
{
    public class PaymentSystemResult
    {
        public bool Success { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
