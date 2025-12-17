using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models;

public class TransactionViewModel
{
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    public CategoryViewModel Category { get; set; }

    public List<SelectListItem> Categories { get; set; }

    public void SetCategories(IEnumerable<CategoryViewModel> categories)
    {
        Categories = categories
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            .ToList();
    }
}
