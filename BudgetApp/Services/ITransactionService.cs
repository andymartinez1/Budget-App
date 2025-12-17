using BudgetApp.Models;

namespace BudgetApp.Services;

public interface ITransactionService
{
    public Task AddTransaction(Transaction transaction);

    public Task<List<Transaction>> GetAllTransactions();

    public Task<Transaction> GetTransactionById(int id);

    public Task UpdateTransaction(int id);

    public Task DeleteTransaction(int id);
}
