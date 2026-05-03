namespace BudgetApp.Entities;

public class Log : BaseEntity
{
    public string Level { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Exception { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
