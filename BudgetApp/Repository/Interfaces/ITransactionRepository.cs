using BudgetApp.Models;

namespace BudgetApp.Repository;

public interface ITransactionRepository
{
    public Task AddTransactionAsync(Transaction transaction);

    public Task<List<Transaction>> GetAllTransactionsAsync();

    public Task<Transaction> GetTransactionByIdAsync(int id);

    public Task UpdateTransactionAsync(Transaction transaction);

    public Task DeleteTransactionAsync(int id);
}
