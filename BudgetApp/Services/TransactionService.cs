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

    public async Task AddTransaction(Transaction transaction)
    {
        if (transaction != null)
            await _repository.AddTransaction(transaction);
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        var transactions = await _repository.GetAllTransactions();
        return transactions;
    }

    public async Task<Transaction> GetTransactionById(int id)
    {
        var transaction = await _repository.GetTransactionById(id);

        return transaction;
    }

    public async Task UpdateTransaction(int id)
    {
        var transactionToUpdate = await _repository.GetTransactionById(id);

        await _repository.UpdateTransaction(transactionToUpdate);
    }

    public async Task DeleteTransaction(int id)
    {
        await _repository.DeleteTransaction(id);
    }
}
