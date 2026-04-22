using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RootCause.Core.Entities;
using RootCause.Core.Enums;
using RootCause.Core.Interfaces;

namespace RootCause.App.ViewModels;

public partial class BugDetailViewModel : ObservableObject
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

    public BugDetailViewModel(IBugService bugService, Bug? bug = null)
    {
        _bugService = bugService;
        if (bug is not null)
        {
            IsEditing = true;
            Title = bug.Title;
            ErrorMessage = bug.ErrorMessage;
            RootCause = bug.RootCause;
            Fix = bug.Fix;
            StackTags = bug.StackTags;
            TimeToSolve = bug.TimeToSolve;
            Serverity = bug.Serverity;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsEditing)
        {
            await _bugService.UpdateBugAsync(
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
        }
        else
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
        }
    }
}
