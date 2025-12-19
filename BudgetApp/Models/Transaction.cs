using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Required]
    public Category Category { get; set; }
}
