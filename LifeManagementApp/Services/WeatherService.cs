using System.Net.Http.Json;
using LifeManagementApp.Models;

namespace LifeManagementApp.Services;

public class WeatherService
{
    private const string Url =
        "https://api.open-meteo.com/v1/forecast?latitude=63.10&longitude=21.62" +
        "&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";

    private readonly HttpClient _http;
    public WeatherService(HttpClient http) => _http = http;

    public async Task<CurrentWeather?> GetCurrentAsync()
    {
        var response = await _http.GetFromJsonAsync<WeatherResponse>(Url);
        return response?.Current;
    }
}