using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models;

public class TransactionViewModel
{
    public Transaction Transaction { get; set; }

    public List<Transaction>? Transactions { get; set; }

    public List<Category> Categories { get; set; }

    public SelectList? TransactionCategories { get; set; }

    public string TransactionCategory { get; set; } = string.Empty;

    public string SearchString { get; set; } = string.Empty;
}
