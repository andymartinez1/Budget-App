using BudgetApp.Data;
using BudgetApp.Entities;
using BudgetApp.Models;
using BudgetApp.ServiceContracts;
using BudgetApp.ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BudgetApp.Services;

public class TransactionService : ITransactionService
{
    private readonly BudgetDbContext _context;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(ILogger<TransactionService> logger, BudgetDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Transaction> AddAsync(TransactionViewModel transactionVm)
    {
        var transaction = new Transaction
        {
            Name = transactionVm.Name,
            Date = transactionVm.Date,
            Amount = transactionVm.Amount,
            CategoryId = transactionVm.CategoryId,
        };

        await _context.Transactions.AddAsync(transaction);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogWarning(e, "Concurrency conflict while adding transaction.");
            return transaction;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Database update failed while adding transaction.");
            return transaction;
        }

        _logger.LogInformation(
            "Transaction '{Name}' created. Amount: ${Amount} on {Date}.",
            transaction.Name,
            transaction.Amount,
            transaction.Date
        );

        return transaction;
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.Include(t => t.Category).AsNoTracking().ToListAsync();
    }

    public async Task<Transaction> GetByIdAsync(int id)
    {
        var transaction = await _context
            .Transactions.Include(t => t.Category)
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id);

        if (transaction is null)
            return null;

        return transaction;
    }

    public async Task<Transaction> UpdateAsync(int id, TransactionViewModel vm)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction is null)
        {
            _logger.LogWarning(
                "Update requested for transaction {TransactionId}, but it was not found.",
                id
            );
            return null;
        }

        transaction.Name = vm.Name;
        transaction.Date = vm.Date;
        transaction.Amount = vm.Amount;
        transaction.CategoryId = vm.CategoryId;

        _context.Transactions.Update(transaction);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogWarning(e, "Concurrency conflict while updating transaction.");
            return transaction;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Database update failed while updating transaction.");
            return transaction;
        }

        _logger.LogInformation("Transaction with ID: {TransactionId} updated.", transaction.Id);

        return transaction;
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction is null)
        {
            _logger.LogWarning(
                "Delete requested for transaction {TransactionId}, but it was not found.",
                id
            );
            return DeleteResult.NotFoundResult("Transaction not found.");
        }

        _context.Transactions.Remove(transaction);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogWarning(
                e,
                "Concurrency conflict while deleting transaction {TransactionId}.",
                id
            );
            return DeleteResult.ConflictResult(
                "The transaction was modified or deleted by another process."
            );
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(
                e,
                "Database update failed while deleting transaction {TransactionId}.",
                id
            );
            return DeleteResult.Failure("Unable to delete the transaction.");
        }

        _logger.LogInformation("Transaction with ID: {TransactionId} deleted.", id);
        return DeleteResult.Success();
    }
}
