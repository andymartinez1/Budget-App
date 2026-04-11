using BudgetApp.Entities;

namespace BudgetApp.Models;

public class CategoryViewModel
{
    public CategoryViewModel(Category category)
    {
        CategoryId = category.Id;
        Type = category.Name;
        Transactions = [];
    }

    public CategoryViewModel() { }

    public int CategoryId { get; set; }

    public string Type { get; set; } = string.Empty;

    public List<TransactionViewModel>? Transactions { get; set; }
}
