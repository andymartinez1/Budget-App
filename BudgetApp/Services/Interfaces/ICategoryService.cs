using BudgetApp.Models;
using BudgetApp.Models.ViewModels;

namespace BudgetApp.Services.Interfaces;

public interface ICategoryService
{
    public Task AddCategoryAsync(CategoryViewModel categoryVm);

    public Task<List<Category>> GetAllCategoriesAsync();

    public Task<Category> GetCategoryByIdAsync(int id);

    public Task UpdateCategoryAsync(int id);

    public Task DeleteCategoryAsync(int id);
}