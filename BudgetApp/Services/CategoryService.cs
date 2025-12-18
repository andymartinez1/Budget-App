using BudgetApp.Models;
using BudgetApp.Repository;

namespace BudgetApp.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task AddCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllCategoriesAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        return category;
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
