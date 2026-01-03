using BudgetApp.Data;
using BudgetApp.Models;
using BudgetApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly BudgetDbContext _dbContext;
    private readonly ILogger<TransactionRepository> _logger;

    public TransactionRepository(BudgetDbContext dbContext, ILogger<TransactionRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        _logger.LogInformation(
            "Transaction '{Name}' created. Amount: ${Amount} on {Date}.",
            transaction.Name,
            transaction.Amount,
            transaction.Date
        );
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

        _logger.LogInformation("Transaction with ID: {TransactionId} retrieved.",transaction.TransactionId);
        return transaction;
    }

    public async Task UpdateTransactionAsync(Transaction transaction)
    {
        _logger.LogInformation("Transaction with ID: {TransactionId} updated.",transaction.TransactionId);
        _dbContext.Transactions.Update(transaction);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(int id)
    {
        var transaction = await GetTransactionByIdAsync(id);

        _logger.LogInformation("Transaction with ID: {TransactionId} deleted.",transaction.TransactionId);
        _dbContext.Transactions.Remove(transaction);

        await _dbContext.SaveChangesAsync();
    }
}
