using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models.ViewModels;

public class TransactionCategoryViewModel
{
    public List<Transaction> Transactions { get; set; } = [];

    public List<Category> Categories { get; set; } = [];

    public List<SelectListItem> CategoriesSelectList { get; set; }

    [Display(Name = "Search")]
    public string? SearchName { get; set; }

    [Display(Name = "Category")]
    public string? FilterCategory { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public string? SearchStartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public string? SearchEndDate { get; set; }

    public int PageIndex { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public void SetCategories(List<Category> categories)
    {
        CategoriesSelectList = categories
            .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Type })
            .ToList();
    }
}
