namespace BudgetApp.Models.ViewModels;

public class CategoryViewModel
{
    public CategoryViewModel(Category category)
    {
        CategoryId = category.CategoryId;
        Type = category.Type;
        Transactions = [];
    }

    public CategoryViewModel() { }

    public int CategoryId { get; set; }

    public string Type { get; set; } = string.Empty;

    public List<TransactionViewModel>? Transactions { get; set; }
}
