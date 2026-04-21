using RootCause.Core.Enums;

namespace RootCause.Core.Entities;

public class Bug
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public string RootCause { get; set; } = string.Empty;

    public string Fix { get; set; } = string.Empty;
    public int TimeToSolve { get; set; }
    public string StackTags { get; set; } = string.Empty; // comma-separated or JSON array
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public Serverity Serverity { get; set; } = Serverity.LOW;
}
