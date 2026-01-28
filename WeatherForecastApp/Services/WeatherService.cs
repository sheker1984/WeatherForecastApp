using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService : IWeatherService
{
    private readonly IDateParser _dateParser;
    private readonly IWeatherClient _weatherClient;
    private readonly IWeatherStorage _storage;
    private readonly IWebHostEnvironment _env;

    public WeatherService(
        IDateParser dateParser,
        IWeatherClient weatherClient,
        IWeatherStorage storage,
        IWebHostEnvironment env)
    {
        _dateParser = dateParser;
        _weatherClient = weatherClient;
        _storage = storage;
        _env = env;
    }

    public async Task<IReadOnlyList<WeatherEntry>> GetWeatherEntriesAsync(CancellationToken cancellationToken = default)
    {
        var datesFile = Path.Combine(_env.ContentRootPath, "dates.txt");
        var parsed = _dateParser.ParseDates(datesFile);

        var results = new List<WeatherEntry>();

        foreach (var item in parsed)
        {
            if (item.Parsed == null)
            {
                results.Add(new WeatherEntry
                {
                    Date = item.Raw,
                    Status = item.Status,
                    ErrorMessage = item.Error
                });
                continue;
            }

            var isoDate = item.Parsed.Value.ToString("yyyy-MM-dd");

            OpenMeteoResponse? response = null;
            string? error = null;

            try
            {
                if (await _storage.ExistsAsync(isoDate))
                {
                    response = await _storage.LoadAsync(isoDate, cancellationToken);
                }
                else
                {
                    var (apiResponse, apiError) = await _weatherClient.GetDailyWeatherAsync(item.Parsed.Value, cancellationToken);
                    if (apiResponse != null)
                    {
                        response = apiResponse;
                        await _storage.SaveAsync(isoDate, apiResponse, cancellationToken);
                    }
                    else
                    {
                        error = apiError ?? "Unknown API error";
                    }
                }
            }
            catch (Exception ex)
            {
                error = $"Storage error: {ex.Message}";
            }

            if (response?.Daily == null ||
                response.Daily.Time == null ||
                response.Daily.Time.Count == 0)
            {
                results.Add(new WeatherEntry
                {
                    Date = isoDate,
                    Status = error == null ? WeatherStatus.NoData : WeatherStatus.ApiError,
                    ErrorMessage = error ?? "No data returned for this date"
                });
                continue;
            }

            var min = response.Daily.TemperatureMin?.FirstOrDefault();
            var max = response.Daily.TemperatureMax?.FirstOrDefault();
            var precip = response.Daily.PrecipitationSum?.FirstOrDefault();

            results.Add(new WeatherEntry
            {
                Date = isoDate,
                MinTemperature = min,
                MaxTemperature = max,
                PrecipitationSum = precip,
                Status = error == null ? WeatherStatus.Success : WeatherStatus.ApiError,
                ErrorMessage = error
            });
        }

        return results;
    }
}
