using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

public partial class TodoViewModel : ObservableObject
{
    private readonly TodoService _todoService;
    private bool _sortByPriority = true;

    public ObservableCollection<TodoItem> Todos { get; } = new();
    public List<int> Priorities { get; } = new() { 1, 2, 3 };

    [ObservableProperty] private string newTitle = "";
    [ObservableProperty] private int newPriority = 2;
    [ObservableProperty] private DateTime newDueDate = DateTime.Today;

    public TodoViewModel(TodoService todoService) => _todoService = todoService;

    [RelayCommand]
    private async Task LoadAsync()
    {
        var all = await _todoService.GetAllAsync();
        var sorted = _sortByPriority
            ? all.OrderBy(t => t.Priority).ThenBy(t => t.DueDate)
            : all.OrderBy(t => t.DueDate).ThenBy(t => t.Priority);

        Todos.Clear();
        foreach (var t in sorted)
            Todos.Add(t);
    }

    [RelayCommand]
    private async Task SortByPriorityAsync() { _sortByPriority = true; await LoadAsync(); }

    [RelayCommand]
    private async Task SortByTimeAsync() { _sortByPriority = false; await LoadAsync(); }

    [RelayCommand]
    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTitle)) return;

        await _todoService.AddAsync(new TodoItem
        {
            Title = NewTitle.Trim(),
            Priority = NewPriority,
            DueDate = NewDueDate
        });
        NewTitle = "";
        await LoadAsync();
    }

    [RelayCommand]
    private async Task DeleteAsync(TodoItem item)
    {
        await _todoService.DeleteAsync(item);
        await LoadAsync();
    }
}