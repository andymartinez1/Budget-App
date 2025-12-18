using BudgetApp.Models;

namespace BudgetApp.Services;

public interface ITransactionService
{
    public Task AddTransactionAsync(Transaction transaction);

    public Task<List<Transaction>> GetAllTransactionsAsync();

    public Task<Transaction> GetTransactionByIdAsync(int id);

    public Task<TransactionDetailsViewModel> GetTransactionDetailsAsync(int id);

    public Task UpdateTransactionAsync(int id);

    public Task DeleteTransactionAsync(int id);
}
