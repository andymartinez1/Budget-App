using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using BudgetApp.Repository.Interfaces;
using BudgetApp.Services.Interfaces;

namespace BudgetApp.Services;

public class TransactionService : ITransactionService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(
        ITransactionRepository transactionRepository,
        ILogger<TransactionService> logger
    )
    {
        _transactionRepository = transactionRepository;
        _logger = logger;
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

        await _transactionRepository.AddTransactionAsync(transaction);
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
        return await _transactionRepository.GetAllTransactionsAsync();
    }

    public async Task<Transaction> GetByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
        _logger.LogInformation("Transaction with ID: {TransactionId} retrieved.", transaction.Id);

        return transaction;
    }

    public async Task<Transaction> UpdateAsync(int id)
    {
        var transaction = await GetByIdAsync(id);

        await _transactionRepository.UpdateTransactionAsync(transaction);
        _logger.LogInformation("Transaction with ID: {TransactionId} updated.", transaction.Id);

        return transaction;
    }

    public async Task DeleteAsync(int id)
    {
        await _transactionRepository.DeleteTransactionAsync(id);
        _logger.LogInformation("Transaction with ID: {TransactionId} deleted.", id);
    }
}
