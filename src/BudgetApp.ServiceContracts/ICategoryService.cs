using BudgetApp.Entities;

namespace BudgetApp.ServiceContracts;

public interface ICategoryService : ICrudService<Category, CategoryViewModel, int> { }
