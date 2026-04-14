namespace BudgetApp.ServiceContracts.DTO;

public sealed record DeleteResult(
    bool Succeeded,
    bool NotFound = false,
    bool Conflict = false,
    string? ErrorMessage = null
)
{
    public static DeleteResult Success() => new(true);

    public static DeleteResult NotFoundResult(string? message = null) =>
        new(false, NotFound: true, ErrorMessage: message);

    public static DeleteResult ConflictResult(string? message = null) =>
        new(false, Conflict: true, ErrorMessage: message);

    public static DeleteResult Failure(string? message = null) => new(false, ErrorMessage: message);
}
