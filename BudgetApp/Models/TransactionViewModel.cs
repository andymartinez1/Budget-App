using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models;

public class TransactionViewModel
{
    public List<Transaction>? Transactions { get; set; }

    public List<Category> Categories { get; set; }

    public SelectList? TransactionCategory { get; set; }

    public string SearchString { get; set; } = string.Empty;
}
