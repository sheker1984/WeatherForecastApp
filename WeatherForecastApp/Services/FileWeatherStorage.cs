using System.Text.Json;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Services;

public class FileWeatherStorage : IWeatherStorage
{
    private readonly string _rootPath;

    public FileWeatherStorage(IWebHostEnvironment env)
    {
        _rootPath = Path.Combine(env.ContentRootPath, "WeatherData");
        Directory.CreateDirectory(_rootPath);
    }

    /// <summary>
    /// Get Path
    /// </summary>
    /// <param name="isoDate"></param>
    /// <returns></returns>
    private string GetPath(string isoDate) => Path.Combine(_rootPath, $"{isoDate}.json");

    /// <summary>
    /// Check Exists
    /// </summary>
    /// <param name="isoDate"></param>
    /// <returns></returns>
    public Task<bool> ExistsAsync(string isoDate)
    {
        var path = GetPath(isoDate);
        return Task.FromResult(File.Exists(path));
    }

    /// <summary>
    /// Save
    /// </summary>
    /// <param name="isoDate"></param>
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SaveAsync(string isoDate, OpenMeteoResponse response, CancellationToken cancellationToken = default)
    {
        var path = GetPath(isoDate);
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(path, json, cancellationToken);
    }

    /// <summary>
    /// Load
    /// </summary>
    /// <param name="isoDate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<OpenMeteoResponse?> LoadAsync(string isoDate, CancellationToken cancellationToken = default)
    {
        var path = GetPath(isoDate);
        if (!File.Exists(path)) return null;

        var json = await File.ReadAllTextAsync(path, cancellationToken);
        return JsonSerializer.Deserialize<OpenMeteoResponse>(json);
    }
}
