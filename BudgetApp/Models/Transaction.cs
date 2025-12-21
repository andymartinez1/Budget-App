using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string Name { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}
