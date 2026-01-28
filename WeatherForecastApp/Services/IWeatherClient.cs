using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IWeatherClient
{
    Task<(OpenMeteoResponse? Response, string? Error)> GetDailyWeatherAsync(DateTime date, CancellationToken cancellationToken = default);
}
