using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Data;

public class SeedCategoryData
{
    public static void InitializeCategories(IServiceProvider serviceProvider)
    {
        using (
            var context = new BudgetDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BudgetDbContext>>()
            )
        )
        {
            if (context.Categories.Any())
                return;

            context.Categories.AddRange(
                new Category { Type = "Income" },
                new Category { Type = "Expense" },
                new Category { Type = "Other" }
            );

            context.SaveChanges();
        }
    }
}