using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models.ViewModels;

public class TransactionCategoryViewModel
{
    public List<Models.Transaction> Transactions { get; set; } = [];

    public SelectList Categories { get; set; }

    [Display(Name = "Filter")]
    public string? SearchName { get; set; }

    [Display(Name = "Category")]
    public string? FilterCategory { get; set; }
}
