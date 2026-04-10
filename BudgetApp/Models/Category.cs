namespace BudgetApp.Models;

public class Category : BaseEntity
{
    public ICollection<Transaction> Transactions { get; set; }
}
