using System.Net.Http.Json;
using System.Web;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class OpenMeteoWeatherClient : IWeatherClient
{
    private readonly HttpClient _httpClient;

    // Dallas, TX
    private const double Latitude = 32.78;
    private const double Longitude = -96.8;

    public OpenMeteoWeatherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://archive-api.open-meteo.com");
    }

    public async Task<(OpenMeteoResponse? Response, string? Error)> GetDailyWeatherAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        var isoDate = date.ToString("yyyy-MM-dd");
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["latitude"] = Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
        query["longitude"] = Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
        query["start_date"] = isoDate;
        query["end_date"] = isoDate;
        query["daily"] = "temperature_2m_max,temperature_2m_min,precipitation_sum";
        query["timezone"] = "auto";

        var url = $"/v1/archive?{query}";

        try
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return (null, $"API error: {response.StatusCode}");
            }

            var data = await response.Content.ReadFromJsonAsync<OpenMeteoResponse>(cancellationToken: cancellationToken);
            return (data, null);
        }
        catch (Exception ex)
        {
            return (null, $"Network/API failure: {ex.Message}");
        }
    }
}
