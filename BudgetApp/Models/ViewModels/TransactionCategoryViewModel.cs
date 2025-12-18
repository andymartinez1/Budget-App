using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models;

public class TransactionCategoryViewModel
{
    public List<Transaction> Transactions { get; set; } = [];

    public List<SelectListItem> Categories { get; set; } = [];

    [Display(Name = "Category")]
    public string? FilterCategory { get; set; }

    public void SetCategories(List<Category> categories)
    {
        Categories =
            (List<SelectListItem>)
                categories.Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.Type,
                });
    }
}
