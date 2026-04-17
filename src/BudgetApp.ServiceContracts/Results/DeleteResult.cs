namespace BudgetApp.ServiceContracts.DTO;

public sealed record DeleteResult(
    bool Succeeded,
    bool NotFound = false,
    bool Conflict = false,
    string? ErrorMessage = null
)
{
    public static DeleteResult Success()
    {
        return new DeleteResult(true);
    }

    public static DeleteResult NotFoundResult(string? message = null)
    {
        return new DeleteResult(false, true, ErrorMessage: message);
    }

    public static DeleteResult ConflictResult(string? message = null)
    {
        return new DeleteResult(false, Conflict: true, ErrorMessage: message);
    }

    public static DeleteResult Failure(string? message = null)
    {
        return new DeleteResult(false, ErrorMessage: message);
    }
}
