using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Models;

public class Transaction
{
    [Key] public int TransactionId { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Now;

    [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")] public decimal? Amount { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}