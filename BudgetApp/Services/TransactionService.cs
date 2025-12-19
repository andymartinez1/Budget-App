using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using BudgetApp.Repository.Interfaces;
using BudgetApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public async Task<TransactionViewModel> AddTransactionAsync(TransactionViewModel transactionVm)
    {
        var transaction = new Transaction
        {
            Name = transactionVm.Name,
            Date = transactionVm.Date,
            Amount = transactionVm.Amount,
            CategoryId = transactionVm.CategoryId,
        };

        await _transactionRepository.AddTransactionAsync(transaction);

        return transactionVm;
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

    public async Task<TransactionViewModel> GetTransactionDetailsAsync(int id)
    {
        var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
        var category = await _categoryRepository.GetCategoryByIdAsync(transaction.CategoryId);

        return new TransactionViewModel
        {
            Amount = transaction.Amount,
            Category = category.Type,
            Date = transaction.Date,
            Name = transaction.Name,
        };
    }

    public async Task<TransactionViewModel> UpdateTransactionAsync(int id)
    {
        var transactionToUpdate = await _transactionRepository.GetTransactionByIdAsync(id);
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        return new TransactionViewModel
        {
            Id = transactionToUpdate.TransactionId,
            Amount = transactionToUpdate.Amount,
            Date = transactionToUpdate.Date,
            Name = transactionToUpdate.Name,
            CategoryId = transactionToUpdate.CategoryId,
            Categories = new SelectList(categories.Select(c => c.Type)),
        };
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _transactionRepository.DeleteTransactionAsync(id);
    }
}
