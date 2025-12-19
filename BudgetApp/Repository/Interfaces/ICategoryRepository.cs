using BudgetApp.Models;

namespace BudgetApp.Repository.Interfaces;

public interface ICategoryRepository
{
    public Task AddCategoryAsync(Category category);

    public Task<List<Category>> GetAllCategoriesAsync();

    public Task<Category> GetCategoryByIdAsync(int id);

    public Task UpdateCategoryAsync(Category category);

    public Task DeleteCategoryAsync(int id);
}
