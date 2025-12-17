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
                    Date = DateTime.Now.AddDays(-2),
                    Name = "Grocery Store",
                    Description = "Weekly groceries",
                    Amount = -76.45m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-30),
                    Name = "Rent",
                    Description = "Monthly rent payment",
                    Amount = -1200.00m,
                    CategoryId = 3,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-12),
                    Name = "Electric Bill",
                    Description = "Utility bill payment",
                    Amount = -89.30m,
                    CategoryId = 4,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-8),
                    Name = "Streaming Subscription",
                    Description = "Monthly streaming service",
                    Amount = -14.99m,
                    CategoryId = 5,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-4),
                    Name = "Gas Station",
                    Description = "Fuel for car",
                    Amount = -40.00m,
                    CategoryId = 6,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-6),
                    Name = "Coffee Shop",
                    Description = "Morning coffee",
                    Amount = -4.75m,
                    CategoryId = 5,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-9),
                    Name = "Restaurant",
                    Description = "Dinner out",
                    Amount = -65.20m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-11),
                    Name = "ATM Withdrawal",
                    Description = "Cash withdrawal",
                    Amount = -100.00m,
                    CategoryId = 6,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-20),
                    Name = "Insurance Refund",
                    Description = "Partial refund received",
                    Amount = 150.00m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-15),
                    Name = "Transfer to Savings",
                    Description = "Monthly savings transfer",
                    Amount = -300.00m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-3),
                    Name = "Farmer's Market",
                    Description = "Fresh produce",
                    Amount = -32.10m,
                    CategoryId = 2,
                }
            );

            context.SaveChanges();
        }
    }
}
