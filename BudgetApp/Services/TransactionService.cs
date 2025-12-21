using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using BudgetApp.Repository.Interfaces;
using BudgetApp.Services.Interfaces;

namespace BudgetApp.Services;

public class TransactionService : ITransactionService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository
    )
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task AddTransactionAsync(TransactionViewModel transactionVm)
    {
        var transaction = new Transaction
        {
            Name = transactionVm.Name,
            Date = transactionVm.Date,
            Amount = transactionVm.Amount,
            CategoryId = transactionVm.CategoryId,
        };

        await _transactionRepository.AddTransactionAsync(transaction);
    }

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        return await _transactionRepository.GetAllTransactionsAsync();
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetTransactionByIdAsync(id);

        return transaction;
    }

    public async Task<Transaction> UpdateTransactionAsync(int id)
    {
        var transaction = await GetTransactionByIdAsync(id);
        await _transactionRepository.UpdateTransactionAsync(transaction);

        return transaction;
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _transactionRepository.DeleteTransactionAsync(id);
    }
}
