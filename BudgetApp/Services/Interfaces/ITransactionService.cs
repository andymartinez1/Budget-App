using BudgetApp.Models;
using BudgetApp.Models.ViewModels;

namespace BudgetApp.Services.Interfaces;

public interface ITransactionService
{
    public Task<TransactionViewModel> AddTransactionAsync(TransactionViewModel transactionVm);

    public Task<List<Transaction>> GetAllTransactionsAsync();

    public Task<Transaction> GetTransactionByIdAsync(int id);

    public Task<TransactionViewModel> GetTransactionDetailsAsync(int id);

    public Task<TransactionViewModel> UpdateTransactionAsync(int id);

    public Task DeleteTransactionAsync(int id);
}
