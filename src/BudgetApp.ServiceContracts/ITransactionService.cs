using BudgetApp.Entities;
using BudgetApp.Models;

namespace BudgetApp.ServiceContracts;

public interface ITransactionService : ICrudService<Transaction, TransactionViewModel, int> { }
