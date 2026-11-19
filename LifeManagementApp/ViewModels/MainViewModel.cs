using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly TodoService _todoService;
    private readonly WeatherService _weatherService;

    public ObservableCollection<TodoItem> TopTodos { get; } = new();

    [ObservableProperty] private string weatherText = "Loading weather...";
    [ObservableProperty] private bool isKiteWeather;

    public MainViewModel(TodoService todoService, WeatherService weatherService)
    {
        _todoService = todoService;
        _weatherService = weatherService;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        TopTodos.Clear();
        foreach (var t in await _todoService.GetTopAsync(3))
            TopTodos.Add(t);

        try
        {
            var w = await _weatherService.GetCurrentAsync();
            if (w != null)
            {
                WeatherText = $"{w.Temperature:0.0} °C, wind {w.WindSpeed:0.0} m/s";
                IsKiteWeather = w.WindSpeed > 5;
            }
        }
        catch
        {
            WeatherText = "Could not load weather";
        }
    }
}