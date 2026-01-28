using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace WeatherApp.Razor.Pages;

public class WeatherModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<WeatherEntry> Entries { get; set; } = new();
    public WeatherEntry? Selected { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsLoading { get; set; } = true;

    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "date";

    [BindProperty(SupportsGet = true)]
    public bool Ascending { get; set; } = true;

    [BindProperty(SupportsGet = true)]
    public string? SelectedDate { get; set; }

    public async Task OnGet()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("WeatherApi");
            var data = await client.GetFromJsonAsync<List<WeatherEntry>>("api/weather");

            Entries = data ?? new List<WeatherEntry>();

            // Sorting
            Entries = SortBy switch
            {
                "min" => Ascending
                    ? Entries.OrderBy(e => e.MinTemperature).ToList()
                    : Entries.OrderByDescending(e => e.MinTemperature).ToList(),

                "max" => Ascending
                    ? Entries.OrderBy(e => e.MaxTemperature).ToList()
                    : Entries.OrderByDescending(e => e.MaxTemperature).ToList(),

                _ => Ascending
                    ? Entries.OrderBy(e => e.Date).ToList()
                    : Entries.OrderByDescending(e => e.Date).ToList(),
            };

            // Row click → details
            if (!string.IsNullOrEmpty(SelectedDate))
            {
                Selected = Entries.FirstOrDefault(e => e.Date == SelectedDate);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load weather data: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public class WeatherEntry
    {
        public string Date { get; set; } = default!;
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public double? PrecipitationSum { get; set; }
        public string Status { get; set; } = default!;
        public string? ErrorMessage { get; set; }
    }
}
