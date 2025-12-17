using BudgetApp.Models;
using BudgetApp.Repository;

namespace BudgetApp.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task AddCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _repository.GetAllCategories();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCategory(int id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }
}
