using BudgetApp.Models;

namespace BudgetApp.Repository;

public interface ICategoryRepository
{
    public Task AddCategory(Category category);

    public Task<List<Category>> GetAllCategories();

    public Task<Category> GetCategoryById(int id);

    public Task UpdateCategory(Category category);

    public Task DeleteCategory(int id);
}
