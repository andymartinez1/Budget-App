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
                    Date = DateTime.Now.AddHours(3.5),
                    Name = "Verizon Bill",
                    Description = "Phone bill",
                    Amount = 79.99m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddHours(24),
                    Name = "Gift",
                    Description = "Birthday gift from a friend",
                    Amount = 150.00m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddHours(26.3),
                    Name = "Donation",
                    Description = "Donation to charity",
                    Amount = 200.00m,
                    CategoryId = 3,
                }
            );

            context.SaveChanges();
        }
    }
}
