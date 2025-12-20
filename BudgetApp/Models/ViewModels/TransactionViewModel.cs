using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models.ViewModels;

public class TransactionViewModel
{
    public int TransactionId { get; set; }

    public DateTime Date { get; set; }

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    [Range(0.01, (double)decimal.MaxValue)]
    [Required]
    public decimal Amount { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public string Category { get; set; }

    public CategoryViewModel CategoryViewModel { get; set; }

    public SelectList Categories { get; set; }
}
