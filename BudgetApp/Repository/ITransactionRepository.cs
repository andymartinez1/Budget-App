using BudgetApp.Models;

namespace BudgetApp.Repository;

public interface ITransactionRepository
{
    public Task AddTransaction(Transaction transaction);

    public Task<List<Transaction>> GetAllTransactions();

    public Task<Transaction> GetTransactionById(int id);

    public Task UpdateTransaction(Transaction transaction);

    public Task DeleteTransaction(int id);
}
