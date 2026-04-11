using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public class VerifyEmailViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
