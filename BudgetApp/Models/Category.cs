using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    public string Type { get; set; } = string.Empty;

    public ICollection<Transaction> Transactions { get; set; }
}
