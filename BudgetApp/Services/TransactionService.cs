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

    public async Task<Transaction> AddTransactionAsync(TransactionViewModel transactionVm)
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

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        return await _transactionRepository.GetAllTransactionsAsync();
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
        _logger.LogInformation(
            "Transaction with ID: {TransactionId} retrieved.",
            transaction.TransactionId
        );

        return transaction;
    }

    public async Task<Transaction> UpdateTransactionAsync(int id)
    {
        var transaction = await GetTransactionByIdAsync(id);

        await _transactionRepository.UpdateTransactionAsync(transaction);
        _logger.LogInformation(
            "Transaction with ID: {TransactionId} updated.",
            transaction.TransactionId
        );

        return transaction;
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _transactionRepository.DeleteTransactionAsync(id);
        _logger.LogInformation("Transaction with ID: {TransactionId} deleted.", id);
    }
}
