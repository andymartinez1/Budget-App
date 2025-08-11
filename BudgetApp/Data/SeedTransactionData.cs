using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Data;

public static class SeedTransactionData
{
    public static void InitializeTransactions(IServiceProvider serviceProvider)
    {
        using (
            var context = new BudgetDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BudgetDbContext>>()
            )
        )
        {
            if (context.Transactions.Any())
                return;

            context.Transactions.AddRange(
                new Transaction
                {
                    Date = DateTime.Now,
                    Name = "Paycheck",
                    Description = "Pay from XYZ company",
                    Amount = 890.20m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddHours(2),
                    Name = "Groceries",
                    Description = "Grocery bill",
                    Amount = 40.31m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddHours(3),
                    Name = "Verizon Bill",
                    Description = "Phone bill",
                    Amount = 79.99m,
                    CategoryId = 2,
                }
            );

            context.SaveChanges();
        }
    }
}
