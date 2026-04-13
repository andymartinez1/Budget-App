using System.ComponentModel.DataAnnotations;
using BudgetApp.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models;

public class TransactionViewModel
{
    public TransactionViewModel() { }

    public TransactionViewModel(List<Category> categories)
    {
        Categories = categories
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToList();
    }

    public TransactionViewModel(Transaction transaction)
    {
        TransactionId = transaction.Id;
        Date = transaction.Date;
        Name = transaction.Name;
        Amount = transaction.Amount;
        CategoryId = transaction.CategoryId;
        Category = transaction.Category;
    }

    public int TransactionId { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    [Range(0.01, (double)decimal.MaxValue)]
    [Required]
    public decimal? Amount { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public string CategoryType { get; set; }

    public Category Category { get; set; }

    public List<SelectListItem> Categories { get; set; }

    public void SetCategories(List<Category> categories)
    {
        Categories = categories
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToList();
    }
}
