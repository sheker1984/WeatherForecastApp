using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class WeatherEntry
    {
        public string Date { get; set; } = default!; // yyyy-MM-dd
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public double? PrecipitationSum { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WeatherStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
