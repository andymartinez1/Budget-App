using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models.ViewModels;

public class TransactionViewModel
{
    public TransactionViewModel() { }

    public TransactionViewModel(List<Category> categories)
    {
        Categories = categories
            .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Type })
            .ToList();
    }

    public TransactionViewModel(Transaction transaction)
    {
        TransactionId = transaction.TransactionId;
        Date = transaction.Date;
        Name = transaction.Name;
        Amount = transaction.Amount;
        CategoryId = transaction.CategoryId;
        Category = transaction.Category;
    }

    public int TransactionId { get; set; }

    public DateTime Date { get; set; }

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    [Range(0.01, (double)decimal.MaxValue)]
    [Required]
    public decimal Amount { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public string CategoryType { get; set; }

    public Category Category { get; set; }

    public List<SelectListItem> Categories { get; set; }

    public void SetCategories(List<Category> categories)
    {
        Categories = categories
            .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Type })
            .ToList();
    }
}
