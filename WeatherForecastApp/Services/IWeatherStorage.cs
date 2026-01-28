using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IWeatherStorage
{
    Task<bool> ExistsAsync(string isoDate);
    Task SaveAsync(string isoDate, OpenMeteoResponse response, CancellationToken cancellationToken = default);
    Task<OpenMeteoResponse?> LoadAsync(string isoDate, CancellationToken cancellationToken = default);
}
