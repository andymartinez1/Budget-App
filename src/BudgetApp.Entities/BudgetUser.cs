using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Entities;

public class BudgetUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
