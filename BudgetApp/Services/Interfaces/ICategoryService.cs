using BudgetApp.Models;
using BudgetApp.Models.ViewModels;

namespace BudgetApp.Services.Interfaces;

public interface ICategoryService : ICrudService<Category, CategoryViewModel, int> { }
