namespace BudgetApp.Models.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<TransactionViewModel>? Transactions { get; set; }
}
