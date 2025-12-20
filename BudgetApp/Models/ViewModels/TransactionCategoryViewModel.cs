using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models.ViewModels;

public class TransactionCategoryViewModel
{
    public List<Transaction> Transactions { get; set; } = [];

    public List<Category> Categories { get; set; }

    public SelectList CategoriesSelectList { get; set; }

    [Display(Name = "Filter")]
    public string? SearchName { get; set; }

    [Display(Name = "Category")]
    public string? FilterCategory { get; set; }
}
