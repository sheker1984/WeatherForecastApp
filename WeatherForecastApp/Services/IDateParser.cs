using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IDateParser
{
    IEnumerable<(string Raw, DateTime? Parsed, WeatherStatus Status, string? Error)> ParseDates(string filePath);
}
