using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

public partial class TodoPage : ContentPage
{
    private readonly TodoViewModel _vm;

    public TodoPage(TodoViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadCommand.ExecuteAsync(null);
    }
}