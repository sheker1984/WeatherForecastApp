using System.Text.Json.Serialization;

namespace WeatherApp.Models;

public class OpenMeteoDaily
{
    [JsonPropertyName("time")]
    public List<string>? Time { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double>? TemperatureMin { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double>? TemperatureMax { get; set; }

    [JsonPropertyName("precipitation_sum")]
    public List<double>? PrecipitationSum { get; set; }
}

public class OpenMeteoResponse
{
    [JsonPropertyName("daily")]
    public OpenMeteoDaily? Daily { get; set; }
}
