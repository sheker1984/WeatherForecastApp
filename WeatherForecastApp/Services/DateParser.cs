using System.Globalization;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class DateParser : IDateParser
{
    private static readonly string[] Formats =
    {
        "MM/dd/yyyy",
        "MMMM d, yyyy",
        "MMM-dd-yyyy",
        "M/d/yyyy",
        "yyyy-MM-dd"
    };

    public IEnumerable<(string Raw, DateTime? Parsed, WeatherStatus Status, string? Error)> ParseDates(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("dates.txt not found", filePath);

        var lines = File.ReadAllLines(filePath)
                        .Where(l => !string.IsNullOrWhiteSpace(l));

        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (DateTime.TryParseExact(trimmed, Formats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var dt))
            {
                yield return (trimmed, dt, WeatherStatus.Success, null);
            }
            else
            {
                yield return (trimmed, null, WeatherStatus.InvalidDate, "Invalid date format or value");
            }
        }
    }
}
