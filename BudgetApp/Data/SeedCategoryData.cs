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
                new Category { Type = "Housing" },
                new Category { Type = "Utilities" },
                new Category { Type = "Food" },
                new Category { Type = "Gift" },
                new Category { Type = "Donation" },
                new Category { Type = "Taxes" },
                new Category { Type = "Transportation" },
                new Category { Type = "Healthcare" },
                new Category { Type = "Entertainment" }
            );

            context.SaveChanges();
        }
    }
}