using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RootCause.Core.Entities;
using RootCause.Core.Interfaces;

namespace RootCause.App.ViewModels;

public partial class BugListViewModel : ObservableObject
{
    private readonly IBugService _bugService;

    [ObservableProperty]
    private ObservableCollection<Bug> _bugs = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private Bug? _selectedBug;

    partial void OnSearchTextChanged(string value)
    {
        FilterBug(value);
    }

    private void FilterBug(string text) { }

    [RelayCommand]
    private async Task LoadBugsAsync()
    {
        Console.WriteLine("Loading bugs");
        var bugs = await _bugService.GetAllBugsAsync();
        Bugs = new ObservableCollection<Bug>(bugs);
        Console.WriteLine($"Loaded {Bugs.Count} bugs");
    }

    public BugListViewModel(IBugService bugService)
    {
        _bugService = bugService;
        _ = LoadBugsAsync();
    }
}
