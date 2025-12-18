using BudgetApp.Models;
using BudgetApp.Repository;

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

    public async Task AddTransactionAsync(Transaction transaction)
    {
        if (transaction != null)
            await _transactionRepository.AddTransactionAsync(transaction);
    }

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        var transactions = await _transactionRepository.GetAllTransactionsAsync();
        return transactions;
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetTransactionByIdAsync(id);

        return transaction;
    }

    public async Task<TransactionDetailsViewModel> GetTransactionDetailsAsync(int id)
    {
        var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
        var category = await _categoryRepository.GetCategoryByIdAsync(transaction.CategoryId);

        return new TransactionDetailsViewModel
        {
            Amount = transaction.Amount,
            Category = category.Type,
            Date = transaction.Date,
            Name = transaction.Name,
        };
    }

    public async Task UpdateTransactionAsync(int id)
    {
        var transactionToUpdate = await _transactionRepository.GetTransactionByIdAsync(id);

        await _transactionRepository.UpdateTransactionAsync(transactionToUpdate);
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _transactionRepository.DeleteTransactionAsync(id);
    }
}
