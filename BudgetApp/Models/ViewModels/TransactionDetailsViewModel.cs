using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Models;

public class TransactionDetailsViewModel
{
    public DateTime Date { get; set; }

    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public string Category { get; set; } = string.Empty;
}
