using BudgetApp.Models;

namespace BudgetApp.Services;

public interface ICategoryService
{
    public Task AddCategory(Category category);

    public Task<List<Category>> GetAllCategories();

    public Task<Category> GetCategoryById(int id);

    public Task UpdateCategory(int id);

    public Task DeleteCategory(int id);
}
