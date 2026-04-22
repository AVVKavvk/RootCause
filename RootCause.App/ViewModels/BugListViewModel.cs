using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RootCause.App.Views;
using RootCause.Core.Entities;
using RootCause.Core.Enums;
using RootCause.Core.Interfaces;

namespace RootCause.App.ViewModels;

public partial class BugListViewModel : ObservableObject
{
    private readonly IBugService _bugService;

    public bool IsEditing { get; set; }

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _rootCause = string.Empty;

    [ObservableProperty]
    private string _fix = string.Empty;

    [ObservableProperty]
    private string _stackTags = string.Empty;

    [ObservableProperty]
    private int _timeToSolve;

    [ObservableProperty]
    private Serverity serverity;

    public Array Severities => Enum.GetValues(typeof(Serverity));

    [ObservableProperty]
    private ObservableCollection<Bug> _bugs = new();

    // Vars
    [ObservableProperty]
    private bool _isCreatePopupOpen;

    [ObservableProperty]
    private Bug? _selectedBug;

    // Filters
    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private SearchTypeEnum _searchType = SearchTypeEnum.Title;

    public Array SearchTypes => Enum.GetValues(typeof(SearchTypeEnum));

    [ObservableProperty]
    private Serverity _searchSeverity = Serverity.LOW;

    [ObservableProperty]
    private DateTimeOffset? _fromDate;

    [ObservableProperty]
    private DateTimeOffset? _toDate;

    partial void OnSearchSeverityChanged(Serverity value)
    {
        Console.WriteLine($"Search severity changed to {value}");
        FilterBug(SearchText);
    }

    partial void OnSearchTextChanged(string value)
    {
        Console.WriteLine($"Search text changed to {value}");
        FilterBug(value);
    }

    partial void OnSearchTypeChanged(SearchTypeEnum value)
    {
        Console.WriteLine($"Search type changed to {value}");
        FilterBug(SearchText);
    }

    partial void OnFromDateChanged(DateTimeOffset? value)
    {
        Console.WriteLine($"From date changed to {value}");
        FilterBug(SearchText);
    }

    partial void OnToDateChanged(DateTimeOffset? value)
    {
        Console.WriteLine($"To date changed to {value}");
        FilterBug(SearchText);
    }

    //   Filters Function
    private void FilterBug(string text)
    {
        Console.WriteLine($"Filtering bug with text {text} with search type {SearchType}");
    }

    [RelayCommand]
    private void RefershBugs() => _ = LoadBugsAsync();

    [RelayCommand]
    private async Task LoadBugsAsync()
    {
        Console.WriteLine("Loading bugs");
        var bugs = await _bugService.GetAllBugsAsync();
        Bugs = new ObservableCollection<Bug>(bugs);
        Console.WriteLine($"Loaded {Bugs.Count} bugs");
    }

    [RelayCommand]
    private void CreateBug()
    {
        IsCreatePopupOpen = true; // open popup
    }

    [RelayCommand]
    private async Task SubmitBugAsync()
    {
        await _bugService.CreateBugAsync(
            new Bug
            {
                Title = Title,
                ErrorMessage = ErrorMessage,
                RootCause = RootCause,
                Fix = Fix,
                StackTags = StackTags,
                TimeToSolve = TimeToSolve,
                Serverity = Serverity,
                CreatedAt = DateTime.Now,
                ResolvedAt = DateTime.Now,
            }
        );

        IsCreatePopupOpen = false; // close popup
        Title = ErrorMessage = RootCause = Fix = StackTags = string.Empty;
        TimeToSolve = 0;
        await LoadBugsAsync();
    }

    [RelayCommand]
    private async Task DeleteBugAsync()
    {
        if (SelectedBug == null)
            return;

        var mainWindow = (
            Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime
        )?.MainWindow;

        var dialog = new Window
        {
            Width = 320,
            Height = 160,
            Title = "Confirm Delete",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false,
            Background = new SolidColorBrush(Color.Parse("#1E1E1E")),
        };

        bool result = false;

        var cancelBtn = new Button { Content = "Cancel" };
        cancelBtn.Click += (_, _) => dialog.Close();

        var deleteBtn = new Button { Content = "Delete", Background = Brushes.Red };
        deleteBtn.Click += (_, _) =>
        {
            result = true;
            dialog.Close();
        };

        dialog.Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            Children =
            {
                new TextBlock
                {
                    Text = $"Delete \"{SelectedBug.Title}\"?",
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap,
                },
                new StackPanel
                {
                    Orientation = Avalonia.Layout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Spacing = 10,
                    Children = { cancelBtn, deleteBtn },
                },
            },
        };

        if (mainWindow != null)
        {
            await dialog.ShowDialog(mainWindow);
        }
        else
        {
            dialog.Show(); // fallback (non-modal)
        }
        ;

        if (!result)
            return;

        await _bugService.DeleteBugAsync(SelectedBug.Id);
        await LoadBugsAsync();
    }

    [RelayCommand]
    private void OpenBugDetails()
    {
        if (SelectedBug == null)
            return;

        var window = new BugDetailsView { DataContext = new BugDetailsViewModel(SelectedBug) };

        window.Show();
    }

    [RelayCommand]
    private void CancelCreate()
    {
        IsCreatePopupOpen = false;
    }

    public BugListViewModel(IBugService bugService)
    {
        _bugService = bugService;
        _ = LoadBugsAsync();
    }
}
