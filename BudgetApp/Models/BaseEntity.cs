using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
    public string Name { get; set; } = string.Empty;
}
