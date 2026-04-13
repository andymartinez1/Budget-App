using BudgetApp.Entities;
using BudgetApp.Models;

namespace BudgetApp.ServiceContracts;

public interface ICategoryService : ICrudService<Category, CategoryViewModel, int> { }
