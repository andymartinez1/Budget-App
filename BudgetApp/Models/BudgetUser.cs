using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Models;

public class BudgetUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
