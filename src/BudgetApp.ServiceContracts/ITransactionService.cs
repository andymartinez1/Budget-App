using BudgetApp.Entities;

namespace BudgetApp.ServiceContracts;

public interface ITransactionService : ICrudService<Transaction, TransactionViewModel, int> { }
