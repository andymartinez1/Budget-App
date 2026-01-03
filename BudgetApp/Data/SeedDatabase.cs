using BudgetApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Data;

public static class SeedDatabase
{
    public static void InitializeTransactions(IServiceProvider serviceProvider)
    {
        using (
            var context = new BudgetDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BudgetDbContext>>()
            )
        )
        {
            var hasher = new PasswordHasher<BudgetUser>();
            context.Users.Add(
                new BudgetUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "test@test.com",
                    NormalizedUserName = "TEST@TEST.COM",
                    Email = "test@test.com",
                    NormalizedEmail = "TEST@TEST.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null, "Password1!"),
                }
            );

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
                new Category { Type = "Entertainment" },
                new Category { Type = "Internal Transfer" },
                new Category { Type = "Other" }
            );

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
                    CategoryId = 4,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-30),
                    Name = "Rent",
                    Amount = -1200.00m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-12),
                    Name = "Electric Bill",
                    Amount = -89.30m,
                    CategoryId = 3,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-8),
                    Name = "Streaming Subscription",
                    Amount = -14.99m,
                    CategoryId = 10,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-4),
                    Name = "Gas Station",
                    Amount = -40.00m,
                    CategoryId = 8,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-6),
                    Name = "Coffee Shop",
                    Amount = -4.75m,
                    CategoryId = 4,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-9),
                    Name = "Restaurant",
                    Amount = -65.20m,
                    CategoryId = 4,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-11),
                    Name = "ATM Withdrawal",
                    Amount = -100.00m,
                    CategoryId = 12,
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
                    CategoryId = 11,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-3),
                    Name = "Farmer's Market",
                    Amount = -32.10m,
                    CategoryId = 4,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-7),
                    Name = "Gym Membership",
                    Amount = -45.00m,
                    CategoryId = 9,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-18),
                    Name = "Phone Bill",
                    Amount = -58.99m,
                    CategoryId = 3,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-25),
                    Name = "Bookstore",
                    Amount = -22.50m,
                    CategoryId = 10,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-14),
                    Name = "Charity Donation",
                    Amount = -100.00m,
                    CategoryId = 6,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-21),
                    Name = "Pet Supplies",
                    Amount = -36.75m,
                    CategoryId = 12,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-29),
                    Name = "Home Improvement",
                    Amount = -250.00m,
                    CategoryId = 2,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-1),
                    Name = "Clothing Store",
                    Amount = -89.99m,
                    CategoryId = 9,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-13),
                    Name = "Pharmacy",
                    Amount = -15.20m,
                    CategoryId = 4,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-16),
                    Name = "Car Repair",
                    Amount = -420.00m,
                    CategoryId = 8,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-23),
                    Name = "Movie Theater",
                    Amount = -27.00m,
                    CategoryId = 10,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-5),
                    Name = "Office Supplies",
                    Amount = -19.60m,
                    CategoryId = 9,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-27),
                    Name = "Lottery Win",
                    Amount = 50.00m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-19),
                    Name = "Gift Received",
                    Amount = 75.00m,
                    CategoryId = 1,
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-10),
                    Name = "Interest",
                    Amount = 5.40m,
                    CategoryId = 1,
                }
            );

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
