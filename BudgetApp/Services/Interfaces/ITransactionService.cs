using BudgetApp.Models;
using BudgetApp.Models.ViewModels;

namespace BudgetApp.Services.Interfaces;

public interface ITransactionService : ICrudService<Transaction, TransactionViewModel, int> { }
