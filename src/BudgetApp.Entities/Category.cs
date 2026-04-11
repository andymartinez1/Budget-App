namespace BudgetApp.Entities;

public class Category : BaseEntity
{
    public ICollection<Transaction> Transactions { get; set; }
}
