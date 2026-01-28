using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IWeatherService
{
    Task<IReadOnlyList<WeatherEntry>> GetWeatherEntriesAsync(CancellationToken cancellationToken = default);
}
