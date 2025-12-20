using BudgetApp.Models;
using BudgetApp.Models.ViewModels;

namespace BudgetApp.Services.Interfaces;

public interface ITransactionService
{
    public Task<Transaction> AddTransactionAsync(TransactionViewModel transactionVm);

    public Task<List<Transaction>> GetAllTransactionsAsync();

    public Task<Transaction> GetTransactionByIdAsync(int id);

    public Task<Transaction> UpdateTransactionAsync(int id);

    public Task DeleteTransactionAsync(int id);
}
