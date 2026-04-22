using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private Bug? _selectedBug;

    [ObservableProperty]
    private bool _isCreatePopupOpen;

    partial void OnSearchTextChanged(string value)
    {
        Console.WriteLine($"Search text changed to {value}");
        FilterBug(value);
    }

    private void FilterBug(string text) { }

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
