using CommunityToolkit.Mvvm.ComponentModel;

namespace RootCause.App.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty]
    private int _totalBugs;

    [ObservableProperty]
    private int _bugThisWeek;
}
