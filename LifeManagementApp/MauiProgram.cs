using LifeManagementApp.Data;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;
using LifeManagementApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace LifeManagementApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif      
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "todos.db");
            builder.Services.AddDbContext<AppDbContext>(
                o => o.UseSqlite($"Filename={dbPath}"), ServiceLifetime.Singleton);

            builder.Services.AddSingleton(new HttpClient());
            builder.Services.AddSingleton<TodoService>();
            builder.Services.AddSingleton<WeatherService>();

            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<TodoViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<TodoPage>();

            return builder.Build();
        }
    }
}
