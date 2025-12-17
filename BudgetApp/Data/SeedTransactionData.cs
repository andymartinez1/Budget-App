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
                    Amount = 890.20m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-2),
                    Name = "Grocery Store",
                    Amount = -76.45m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-30),
                    Name = "Rent",
                    Amount = -1200.00m,
                    CategoryId = 3,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-12),
                    Name = "Electric Bill",
                    Amount = -89.30m,
                    CategoryId = 4,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-8),
                    Name = "Streaming Subscription",
                    Amount = -14.99m,
                    CategoryId = 5,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-4),
                    Name = "Gas Station",
                    Amount = -40.00m,
                    CategoryId = 6,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-6),
                    Name = "Coffee Shop",
                    Amount = -4.75m,
                    CategoryId = 5,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-9),
                    Name = "Restaurant",
                    Amount = -65.20m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-11),
                    Name = "ATM Withdrawal",
                    Amount = -100.00m,
                    CategoryId = 6,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-20),
                    Name = "Insurance Refund",
                    Amount = 150.00m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-15),
                    Name = "Transfer to Savings",
                    Amount = -300.00m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-3),
                    Name = "Farmer's Market",
                    Amount = -32.10m,
                    CategoryId = 2,
                }
            );

            context.SaveChanges();
        }
    }
}
