using BudgetApp.Data;
using BudgetApp.Models;
using BudgetApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly BudgetDbContext _dbContext;

    public TransactionRepository(BudgetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        return await _dbContext.Transactions.ToListAsync();
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        var transaction = await _dbContext.Transactions.FindAsync(id);

        return transaction;
    }

    public async Task UpdateTransactionAsync(Transaction transaction)
    {
        _dbContext.Transactions.Update(transaction);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(int id)
    {
        var transaction = await GetTransactionByIdAsync(id);

        _dbContext.Transactions.Remove(transaction);

        await _dbContext.SaveChangesAsync();
    }
}
