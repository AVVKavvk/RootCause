using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RootCause.Core.Entities;
using RootCause.Core.Interfaces;

namespace RootCause.App.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IServiceProvider _services;

    // Current visible page
    [ObservableProperty]
    private ObservableObject _currentPage;

    public DashboardViewModel Dashboard { get; }

    public BugListViewModel BugList { get; }

    public StatsViewModel Stats { get; }

    public MainWindowViewModel(
        DashboardViewModel dashboard,
        BugListViewModel bugList,
        StatsViewModel stats,
        IServiceProvider services
    )
    {
        Dashboard = dashboard;
        BugList = bugList;
        Stats = stats;

        _services = services;

        // Default page on startup
        _currentPage = Dashboard;
    }

    // Navigation commands

    [RelayCommand]
    private void NavigateToDashboard()
    {
        CurrentPage = Dashboard;
    }

    [RelayCommand]
    private void NavigateToBugList() => CurrentPage = BugList;

    [RelayCommand]
    private void NavigateToStats() => CurrentPage = Stats;

    // BugDetail needs a bug passed in
    // [RelayCommand]
    // private void NavigateToBugDetail(Bug? bug = null)
    // {
    //     var bugService = _services.GetRequiredService<IBugService>();
    //     var detail = new BugDetailViewModel(bugService, bug); // new or edit
    //     CurrentPage = detail;
    // }
}
