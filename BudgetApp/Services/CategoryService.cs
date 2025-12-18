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

    public async Task AddCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _repository.GetAllCategoriesAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }
}
