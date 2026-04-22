using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RootCause.App.Extensions;
using RootCause.App.ViewModels;
using RootCause.App.Views;
using RootCause.Data.Context;
using RootCause.Data.Extensions;
using RootCause.Data.Helpers;
using RootCause.Services.Extensions;

namespace RootCause.App;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        var connectionString = DatabaseHelper.GetConnectionString();

        // Data Injection
        var collection = new ServiceCollection();

        collection.AddDataServices(connectionString);
        collection.AddServices();
        collection.AddViewModels();

        var services = collection.BuildServiceProvider();
        collection.AddSingleton<IServiceProvider>(services);

        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BugDBContext>();
        await db.Database.MigrateAsync();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = // Get it from DI — not new!
                DataContext =
                    services.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
