using System;
using RootCause.Core.Entities;
using RootCause.Core.Enums;

namespace RootCause.App.ViewModels;

public class BugDetailsViewModel
{
    private readonly Bug _bug;

    public BugDetailsViewModel(Bug bug)
    {
        _bug = bug;
    }

    public string Title => _bug.Title;
    public string ErrorMessage => _bug.ErrorMessage;
    public string RootCause => _bug.RootCause;
    public string Fix => _bug.Fix;
    public string StackTags => _bug.StackTags;
    public Serverity Serverity => _bug.Serverity;
    public DateTime CreatedAt => _bug.CreatedAt;
}
