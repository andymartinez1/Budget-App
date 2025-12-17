using BudgetApp.Data;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly BudgetDbContext _dbContext;

    public TransactionRepository(BudgetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTransaction(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        return await _dbContext.Transactions.ToListAsync();
    }

    public async Task<Transaction> GetTransactionById(int id)
    {
        var transaction = await _dbContext.Transactions.FindAsync(id);

        return transaction;
    }

    public async Task UpdateTransaction(Transaction transaction)
    {
        _dbContext.Transactions.Update(transaction);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTransaction(int id)
    {
        var transaction = await GetTransactionById(id);

        _dbContext.Transactions.Remove(transaction);

        await _dbContext.SaveChangesAsync();
    }
}
