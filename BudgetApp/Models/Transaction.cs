using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Now;

    public string Name { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}
