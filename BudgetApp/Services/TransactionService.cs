using BudgetApp.Models;
using BudgetApp.Repository;

namespace BudgetApp.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        if (transaction != null)
            await _repository.AddTransactionAsync(transaction);
    }

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        var transactions = await _repository.GetAllTransactionsAsync();
        return transactions;
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        var transaction = await _repository.GetTransactionByIdAsync(id);

        return transaction;
    }

    public async Task UpdateTransactionAsync(int id)
    {
        var transactionToUpdate = await _repository.GetTransactionByIdAsync(id);

        await _repository.UpdateTransactionAsync(transactionToUpdate);
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _repository.DeleteTransactionAsync(id);
    }
}
